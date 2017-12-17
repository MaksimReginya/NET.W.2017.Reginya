using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF.Mappers;
using DAL.Interface;
using DAL.Interface.DTO;
using ORM.Models;

namespace DAL.EF
{
    /// <inheritdoc />
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class DatabaseRepository : IBankAccountRepository
    {
        #region Private fields

        private readonly DbContext _context;

        #endregion

        #region Public constructors

        public DatabaseRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region IStorage implementation

        /// <inheritdoc />
        public void AddAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }
                
            if (GetAccountByNumber(account.AccountNumber) != null)
            {
                throw new RepositoryException("Account already exists in repository.");
            }

            var ormAccount = account.ToOrmAccount();
            SetTypeAndOwner(ormAccount);
            _context.Set<Account>().Add(ormAccount);                                     
        }

        /// <inheritdoc />
        public DtoAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }
            
            return _context.Set<Account>()
                .Include(account => account.AccountOwner)
                .Include(account => account.AccountType)
                .FirstOrDefault(account => account.AccountNumber == accountNumber)
                ?.ToDtoAccount();                
        }

        /// <inheritdoc />
        public void UpdateAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }
            
            var updatingAccount = GetAccountByNumber(account.AccountNumber);
            if (ReferenceEquals(updatingAccount, null))
            {
                throw new RepositoryException("Updating account does not exist.");
            }

            UpdateAccount(updatingAccount, account);                        
        }
        
        /// <inheritdoc />
        public void RemoveAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }
            
            var removingAccount = GetAccountByNumber(account.AccountNumber);
            if (ReferenceEquals(removingAccount, null))
            {
                throw new RepositoryException("Removing account can't be found in repository.");
            }

            _context.Set<Account>().Remove(removingAccount);
        }

        /// <inheritdoc />
        public IEnumerable<DtoAccount> GetAllAccounts()
            => _context.Set<Account>()
                .Include(account => account.AccountOwner)
                .Include(account => account.AccountType)
                .ToList()
                .Select(account => account.ToDtoAccount());                    

        #endregion

        #region Private methods

        private AccountOwner GetAccountOwnerByName(string firstName, string lastName, string email)
            => _context.Set<AccountOwner>().FirstOrDefault(
                owner => owner.FirstName == firstName && owner.LastName == lastName && owner.Email == email);        

        private AccountType GetAccountTypeByName(string accountTypeName)
            => _context.Set<AccountType>().FirstOrDefault(accountType => accountType.Name == accountTypeName);        

        private Account GetAccountByNumber(string accountNumber)
            => _context.Set<Account>().FirstOrDefault(account => account.AccountNumber == accountNumber);

        private void SetTypeAndOwner(Account account)
        {
            var accountOwner = GetAccountOwnerByName(
                account.AccountOwner.FirstName,
                account.AccountOwner.LastName,
                account.AccountOwner.Email);

            if (accountOwner != null)
            {
                account.AccountOwner = null;
                account.AccountOwnerId = accountOwner.Id;
            }

            var accountType = GetAccountTypeByName(account.AccountType.Name);

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
