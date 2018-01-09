using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Mappers;
using ORM.Models;

namespace DAL
{
    /// <inheritdoc />
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class BankAccountRepository : IBankAccountRepository
    {
        #region Private fields

        private readonly DbContext _context;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountRepository"/> class.
        /// </summary>
        /// <param name="context">Database context with accounts.</param>
        public BankAccountRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region IStorage implementation

        /// <inheritdoc />
        public void AddAccount(DtoAccount account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                if (GetAccountByNumber(account.AccountNumber) != null)
                {
                    throw new RepositoryException("Account already exists in repository.");
                }

                var accountOwner = GetOwnerByEmail(account.AccountOwner.Email);
                var ormAccount = account.ToOrmAccount(accountOwner);
                _context.Set<Account>().Add(ormAccount);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in adding new account.", ex);
            }            
        }

        /// <inheritdoc />
        public DtoAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            try
            {
                var accountOwner = _context
                    .Set<AccountOwner>()
                    .Include(owner => owner.Accounts)
                    .FirstOrDefault(owner => owner.Accounts.Any(account => account.AccountNumber == accountNumber));

                if (accountOwner is null)
                {
                    throw new RepositoryException("Account owner can't be found.");
                }

                return accountOwner.Accounts
                    .FirstOrDefault(account => account.AccountNumber == accountNumber)
                    ?.ToDtoAccount(accountOwner.ToDtoAccountOwner());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in getting account.", ex);
            }            
        }

        /// <inheritdoc />
        public void UpdateAccount(DtoAccount account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                var updatingAccount = GetAccountByNumber(account.AccountNumber);
                if (updatingAccount is null)
                {
                    throw new RepositoryException("Updating account does not exist.");
                }

                UpdateAccount(updatingAccount, account);
                _context.Entry(updatingAccount).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in updating account.", ex);
            }
        }
        
        /// <inheritdoc />
        public void RemoveAccount(DtoAccount account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                var removingAccount = GetAccountByNumber(account.AccountNumber);
                if (removingAccount is null)
                {
                    throw new RepositoryException("Removing account can't be found in repository.");
                }

                _context.Set<Account>().Remove(removingAccount);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in removing account.", ex);
            }
        }

        /// <inheritdoc />
        public IEnumerable<DtoAccount> GetAllAccounts()
        {
            try
            {
                return _context.Set<Account>()
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .ToList()
                    .Select(account => account.ToDtoAccount(account.AccountOwner.ToDtoAccountOwner()));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in getting all account.", ex);
            }
        }

        #endregion

        #region Private methods

        private static void UpdateAccount(Account updatingAccount, DtoAccount account)
        {
            updatingAccount.Balance = account.Balance;
            updatingAccount.Bonus = account.Bonus;
        }

        private Account GetAccountByNumber(string accountNumber)
            => _context.Set<Account>().FirstOrDefault(account => account.AccountNumber == accountNumber);                             
               
        private AccountOwner GetOwnerByEmail(string email)
        {
            var result = _context
                .Set<AccountOwner>()
                .Include(owner => owner.Accounts)
                .FirstOrDefault(owner => owner.Email == email);            

            return result;
        }

        #endregion
    }
}
