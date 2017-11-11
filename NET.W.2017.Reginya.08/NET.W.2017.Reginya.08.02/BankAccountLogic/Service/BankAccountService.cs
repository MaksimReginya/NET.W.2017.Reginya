using System;
using BankAccountLogic.AccountTypes;

namespace BankAccountLogic.Service
{
    /// <summary>
    /// Provides methods to work with bank accounts
    /// </summary>
    public class BankAccountService : IService
    {
        #region Private fields        

        private readonly IStorage _storage;        

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountService"/> class.
        /// </summary>
        /// <param name="storage">account repository service</param>              
        public BankAccountService(IStorage storage)
        {
            if (ReferenceEquals(storage, null))
            {
                throw new ArgumentNullException(nameof(storage));
            }         

            _storage = storage;
        }

        #endregion 

        #region IService implementation
       
        /// <inheritdoc />
        public string CreateAccount(Type type, string ownerFirstName, string ownerLastName, decimal balance = 0m, int bonus = 0)
        {
            IsValidInput(type, ownerFirstName, ownerLastName, balance, bonus);

            string accountNumber = AccountNumberGenerator.GenerateNumber(_storage);

            var account = Activator.CreateInstance(type, accountNumber, ownerFirstName, ownerLastName, balance, bonus) as BankAccount;

            _storage.AddAccount(account);

            return accountNumber;
        }

        /// <inheritdoc />
        public void Deposit(string accountNumber, decimal value)
        {
            _storage.GetAccount(accountNumber).Deposit(value);
        }

        /// <inheritdoc />
        public void Withdraw(string accountNumber, decimal value)
        {
            _storage.GetAccount(accountNumber).Withdraw(value);
        }

        /// <inheritdoc />
        public void CloseAccount(string accountNumber)
        {
            _storage.RemoveAccount(_storage.GetAccount(accountNumber));
        }

        /// <inheritdoc />
        public string GetAccountInfo(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            return _storage.GetAccount(accountNumber).ToString();            
        }

        #endregion 

        #region Private methods

        private static void IsValidInput(Type type, string ownerFirstName, string ownerLastName, decimal balance, int bonus)
        {
            if (!type.IsSubclassOf(typeof(BankAccount)))
            {
                throw new ArgumentException("Invalid account type", nameof(type));
            }

            if (string.IsNullOrWhiteSpace(ownerFirstName))
            {
                throw new ArgumentException($"{nameof(ownerFirstName)} can't be empty.", nameof(ownerFirstName));
            }

            if (string.IsNullOrWhiteSpace(ownerLastName))
            {
                throw new ArgumentException($"{nameof(ownerLastName)} can't be empty.", nameof(ownerLastName));
            }

            if (balance < 0)
            {
                throw new ArgumentException("Account balance must be positive", nameof(balance));
            }

            if (bonus < 0)
            {
                throw new ArgumentException("Bonus must be positive", nameof(bonus));
            }
        }
                                                
        #endregion 
    }
}
