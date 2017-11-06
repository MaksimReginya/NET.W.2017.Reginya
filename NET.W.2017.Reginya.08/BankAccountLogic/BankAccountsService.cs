using System;
using System.Collections.Generic;
using System.Linq;
using BankAccountLogic.AccountTypes;
using BankAccountLogic.Exceptions;
using BankAccountLogic.Predicates;

namespace BankAccountLogic
{
    /// <summary>
    /// Provides methods to work with list of accounts
    /// </summary>
    public class BankAccountsService
    {
        #region Private fields

        private List<BankAccount> _accounts;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountsService"/> class.
        /// </summary>
        public BankAccountsService()
        {
            _accounts = new List<BankAccount>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountsService"/> class.
        /// </summary>
        /// <param name="accounts">The list of accounts</param>
        public BankAccountsService(IEnumerable<BankAccount> accounts)
        {
            _accounts = new List<BankAccount>(accounts);
        }

        #endregion

        #region Public properties

        public int AccountsCount => _accounts.Count;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates and adds new account to list.
        /// </summary>
        /// <param name="account"> The account to add to list. </param>
        /// <exception cref="ArgumentNullException">Thrown when account is null</exception>
        /// <exception cref="AccountAlreadyExistsException">Thrown when account is already in list</exception>
        public void Create(BankAccount account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (_accounts.Contains(account))
                throw new AccountAlreadyExistsException(account);

            _accounts.Add(account);
        }

        /// <summary>
        /// Removes the account from the list.
        /// </summary>
        /// <param name="accountNumber"> The account number to remove from the list. </param>
        /// <exception cref="ArgumentNullException">Thrown when account number is null</exception>
        /// <exception cref="AccountNotFoundException">Thrown when account can't be found in list</exception>
        public void Remove(string accountNumber)
        {
            if (accountNumber == null)
                throw new ArgumentNullException(nameof(accountNumber));

            var predicate = new IsNumberValid(accountNumber);
            if (FindAccountByTag(predicate) == null)
                throw new AccountNotFoundException(accountNumber);

            _accounts.RemoveAll((acc) => acc.AccountNumber.Equals(accountNumber));                               
        }

        /// <summary>
        /// Searches for the first element that matches the passed condition.
        /// </summary>
        /// <param name="condition">
        ///  The <see cref="IPredicate{T}"/> instance that defines the condition.
        /// </param>
        /// <returns> The first element that matches the passed condition. </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when condition is null
        /// </exception>
        public BankAccount FindAccountByTag(IPredicate<BankAccount> condition)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            return _accounts.Find(condition.IsTrue);
        }

        /// <summary>
        /// Saves accounts into <see cref="storage"/>.
        /// </summary>
        /// <param name="storage"> Storage of the accounts. </param>
        /// <exception cref="ArgumentNullException">Thrown when storage is null</exception>
        public void Save(IAccountsStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            storage.Save(_accounts);
        }

        /// <summary>
        /// Load accounts from <see cref="storage"/>.
        /// </summary>
        /// <param name="storage"> Storage to load a list of accounts. </param>
        /// <exception cref="ArgumentNullException">Thrown when storage is null </exception>
        /// <exception cref="ArgumentException">Thrown when account list can't be loaded from storage</exception>
        public void Load(IAccountsStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _accounts = storage.Load() as List<BankAccount>;

            if (_accounts == null)
                throw new ArgumentException($"Can't load account list from {nameof(storage)}.");
        }

        /// <summary>
        /// Returns list of accounts.
        /// </summary>
        /// <returns> Returns <see cref="IEnumerable{Book}"/> of accounts. </returns>
        public IEnumerable<BankAccount> GetAccounts() => new List<BankAccount>(_accounts);

        #endregion
    }
}
