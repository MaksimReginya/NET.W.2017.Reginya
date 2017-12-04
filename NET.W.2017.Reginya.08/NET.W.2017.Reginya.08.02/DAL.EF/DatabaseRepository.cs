using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF.Mappers;
using DAL.EF.Models;
using DAL.Interface;
using DAL.Interface.DTO;

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
                if (GetAccountByNumber(db, account.AccountNumber) == null)
                {
                    throw new RepositoryException("Account already exists in repository.");
                }

                SetTypeAndOwner(db, account.ToOrmAccount());
                db.Entry(account).State = EntityState.Added;
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
                return db.Accounts
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

                db.Entry(removingAccount).State = EntityState.Deleted;                
                db.SaveChanges();
            }            
        }

        /// <inheritdoc />
        public IEnumerable<DtoAccount> GetAllAccounts()
        {            
            using (var db = new AccountContext())
            {                
                return db.Accounts
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .ToList()
                    .Select(account => account.ToDtoAccount());
            }            
        }

        #endregion

        #region Private methods

        private static AccountOwner GetAccountOwnerByName(AccountContext db, string firstName, string lastName)
        {
            return db.AccountOwners.FirstOrDefault(owner => owner.FirstName == firstName && owner.LastName == lastName);
        }

        private static AccountType GetAccountTypeByName(AccountContext db, string accountTypeName)
            => db.AccountTypes.FirstOrDefault(accountType => accountType.Name == accountTypeName);        

        private static Account GetAccountByNumber(AccountContext db, string accountNumber)
            => db.Accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);

        private static void SetTypeAndOwner(AccountContext db, Account account)
        {
            var accountOwner = GetAccountOwnerByName(db, account.AccountOwner.FirstName, account.AccountOwner.LastName);

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
