using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF.Mappers;
using DAL.Interface;
using DAL.Interface.DTO;
using ORM;
using ORM.Models;

namespace DAL.EF
{
    /// <inheritdoc />
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class DatabaseRepository : IBankAccountRepository
    {       
        #region IStorage implementation

        /// <inheritdoc />
        public void AddAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }
                            
            using (var db = new AccountContext())
            {
                if (GetAccountByNumber(db, account.AccountNumber) != null)
                {
                    throw new RepositoryException("Account already exists in repository.");
                }

                var ormAccount = account.ToOrmAccount();
                SetTypeAndOwner(db, ormAccount);
            
                db.Set<Account>().Add(ormAccount);
                db.SaveChanges();
            }                                              
        }

        /// <inheritdoc />
        public DtoAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            using (var db = new AccountContext())
            {
                return db.Set<Account>()
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .FirstOrDefault(account => account.AccountNumber == accountNumber)
                    ?.ToDtoAccount();
            }
        }

        /// <inheritdoc />
        public void UpdateAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                var updatingAccount = GetAccountByNumber(db, account.AccountNumber);
                if (ReferenceEquals(updatingAccount, null))
                {
                    throw new RepositoryException("Updating account does not exist.");
                }

                UpdateAccount(updatingAccount, account);
                db.SaveChanges();
            }                                 
        }
        
        /// <inheritdoc />
        public void RemoveAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                var removingAccount = GetAccountByNumber(db, account.AccountNumber);
                if (ReferenceEquals(removingAccount, null))
                {
                    throw new RepositoryException("Removing account can't be found in repository.");
                }
            
                db.Set<Account>().Remove(removingAccount);
                db.SaveChanges();
            }
        }

        /// <inheritdoc />
        public IEnumerable<DtoAccount> GetAllAccounts()
        {
            using (var db = new AccountContext())
            {
                return db.Set<Account>()
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .ToList()
                    .Select(account => account.ToDtoAccount());
            }
        }

        #endregion

        #region Private methods

        private AccountOwner GetAccountOwnerByName(DbContext db, string firstName, string lastName, string email)
            => db.Set<AccountOwner>().FirstOrDefault(
                owner => owner.FirstName == firstName && owner.LastName == lastName && owner.Email == email);                 

        private AccountType GetAccountTypeByName(DbContext db, string accountTypeName)                
            => db.Set<AccountType>().FirstOrDefault(accountType => accountType.Name == accountTypeName);                    

        private Account GetAccountByNumber(DbContext db, string accountNumber)
            => db.Set<Account>().FirstOrDefault(account => account.AccountNumber == accountNumber);                             

        private void SetTypeAndOwner(DbContext db, Account account)
        {
            var accountOwner = GetAccountOwnerByName(
                db,
                account.AccountOwner.FirstName,
                account.AccountOwner.LastName,
                account.AccountOwner.Email);

            if (accountOwner != null)
            {
                account.AccountOwner = null;
                account.AccountOwnerId = accountOwner.Id;
            }

            var accountType = GetAccountTypeByName(db, account.AccountType.Name);

            if (accountType != null)
            {
                account.AccountType = null;
                account.AccountTypeId = accountType.Id;
            }
        }

        private static void UpdateAccount(Account updatingAccount, DtoAccount account)
        {
            updatingAccount.Balance = account.Balance;
            updatingAccount.Bonus = account.Bonus;                                    
        }

        #endregion
    }
}
