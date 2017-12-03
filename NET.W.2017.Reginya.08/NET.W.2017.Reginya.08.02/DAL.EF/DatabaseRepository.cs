using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {    
                if (db.Accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
                {
                    throw new RepositoryException("Account already exists in repository.");
                }

                db.Accounts.Add(GetAccount(account));
                db.SaveChanges();                                                  
            }           
        }

        /// <inheritdoc />
        public DalAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            using (var db = new AccountContext())
            {                
                var account = db.Accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
                var dalAccount = GetDalAccount(account);
                return ReferenceEquals(dalAccount, null) ? null : dalAccount;                
            }           
        }

        /// <inheritdoc />
        public void UpdateAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                var updatingAccount = db.Accounts.FirstOrDefault(dalAccount => dalAccount.AccountNumber == account.AccountNumber);
                if (ReferenceEquals(updatingAccount, null))
                {
                    throw new RepositoryException("Updating account does not exist.");
                }

                UpdateAccount(updatingAccount, account);
                db.SaveChanges();                
            }            
        }
        
        /// <inheritdoc />
        public void RemoveAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                var removingAccount = db.Accounts.FirstOrDefault(dalAccount => dalAccount.AccountNumber == account.AccountNumber);
                if (ReferenceEquals(removingAccount, null))
                {
                    throw new RepositoryException("Removing account can't be found in repository.");
                }

                db.Accounts.Remove(removingAccount);                
                db.SaveChanges();
            }            
        }

        /// <inheritdoc />
        public IEnumerable<DalAccount> GetAllAccounts()
        {
            var accounts = new List<DalAccount>();
            using (var db = new AccountContext())
            {                
                accounts.AddRange(db.Accounts.ToList().Select(GetDalAccount));
            }

            return accounts;
        }

        #endregion

        #region Private methods

        private static Account GetAccount(DalAccount account) =>
            new Account
            {
                AccountType = account.AccountType,
                Balance = account.Balance,
                Bonus = account.Bonus,
                AccountNumber = account.AccountNumber,
                OwnerFirstName = account.OwnerFirstName,
                OwnerLastName = account.OwnerLastName
            };

        private static DalAccount GetDalAccount(Account account) =>
            new DalAccount
            {
                AccountType = account.AccountType,
                Balance = account.Balance,
                Bonus = account.Bonus,
                AccountNumber = account.AccountNumber,
                OwnerFirstName = account.OwnerFirstName,
                OwnerLastName = account.OwnerLastName
            };

        private static void UpdateAccount(Account updatingAccount, DalAccount account)
        {
            updatingAccount.Balance = account.Balance;
            updatingAccount.Bonus = account.Bonus;
            updatingAccount.OwnerFirstName = account.OwnerFirstName;
            updatingAccount.OwnerLastName = account.OwnerLastName;
            updatingAccount.AccountType = account.AccountType;
        }

        #endregion
    }
}
