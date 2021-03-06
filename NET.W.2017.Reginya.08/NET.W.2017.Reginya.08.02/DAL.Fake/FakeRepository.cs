﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;

namespace DAL.Fake
{
    /// <inheritdoc />
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class FakeRepository : IBankAccountRepository
    {
        #region Private fields
        
        private readonly List<DtoAccount> _accounts;

        #endregion         

        #region Public constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRepository" /> class.
        /// </summary>
        public FakeRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Path to file is invalid.", nameof(filePath));
            }
            
            _accounts = new List<DtoAccount>();
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

            if (_accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
            {
                throw new RepositoryException("Account already exists in repository.");
            }
            
            _accounts.Add(account);
        }

        /// <inheritdoc />
        public DtoAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            if (!_accounts.Any(account => account.AccountNumber == accountNumber))
            {
                throw new RepositoryException("Account can't be found in repository.");
            }

            return _accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
        }

        /// <inheritdoc />
        public void UpdateAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
            {
                throw new RepositoryException("Account can't be found in repository.");
            }

            _accounts.RemoveAll(dalAccount => dalAccount.AccountNumber == account.AccountNumber);
            _accounts.Add(account);            
        }

        /// <inheritdoc />
        public void RemoveAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
            {
                throw new RepositoryException("Account can't be found in repository.");
            }

            _accounts.RemoveAll(dalAccount => dalAccount.AccountNumber == account.AccountNumber);
        }

        /// <inheritdoc />
        public IEnumerable<DtoAccount> GetAllAccounts() =>
            new List<DtoAccount>(_accounts);
        
        #endregion
    }
}
