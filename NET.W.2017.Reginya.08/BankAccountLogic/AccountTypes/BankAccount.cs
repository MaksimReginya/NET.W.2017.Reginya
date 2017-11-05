using System;
using System.Text.RegularExpressions;

namespace BankAccountLogic.AccountTypes
{
    /// <summary>
    /// Base class for all types of bank accounts
    /// </summary>
    public abstract class BankAccount
    {
        #region Private fields

        private string _accountNumber;
        private string _ownerFirstName;
        private string _ownerLastName;
        private double _balance;
        private double _bonus;

        #endregion

        #region Public fields

        /// <summary>
        /// Account number
        /// </summary>
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (!IsAccountNumberValid(value))
                    throw new ArgumentException("Account number is not valid.", nameof(AccountNumber));
                _accountNumber = value;
            }
        }
        /// <summary>
        /// Owner's first name
        /// </summary>
        public string OwnerFirstName
        {
            get => _ownerFirstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"{nameof(OwnerFirstName)} can't be empty.");
                _ownerFirstName = value;
            }
        }
        /// <summary>
        /// Owner's last name
        /// </summary>
        public string OwnerLastName
        {
            get => _ownerLastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"{nameof(OwnerLastName)} can't be empty.");
                _ownerLastName = value;
            }
        }
        /// <summary>
        /// Balance of the account
        /// </summary>
        public double Balance
        {
            get => _balance;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException($"{Balance} can not be negative.");
                _balance = value;
            }
        }
        /// <summary>
        /// Bonus of the account
        /// </summary>
        public double Bonus
        {
            get => _bonus;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException($"{Bonus} can not be negative.");
                _bonus = value;
            }
        }

        #endregion
        
        #region Public methods

        /// <summary>
        /// Determines if value is valid account number
        /// </summary>
        /// <param name="value">Number to check</param>
        /// <returns>
        /// True if number is valid, false - if not.
        /// </returns>
        public virtual bool IsAccountNumberValid(string value)
        {
            if (value.Length != 20) return false;
            var regex = new Regex(@"[0-9]{5}[a-z]{4}[0-9]{11}");
            return regex.IsMatch(value);
        }

        /// <summary>
        /// Performs decreasing of account balance on value
        /// </summary>
        /// <param name="value">Value to decrease balance</param>
        public virtual void Withdraw(double value)
        {
            if (Balance - value < 0)
                throw new ArgumentException("Withdraw value can't be bigger than balance.");

            Balance -= value;

            DecreaseBonus();
        }        

        /// <summary>
        /// Performs increasing of account balance on value
        /// </summary>
        /// <param name="value">Value to increase balance</param>
        public virtual void Replenish(double value)
        {
            if (value >= double.MaxValue - Balance)
                throw new ArgumentException("Value to increase balance is too big.");

            Balance += value;

            IncreaseBonus();
        }

        #endregion

        #region Protected methods

        protected virtual void DecreaseBonus() { }

        protected virtual void IncreaseBonus() { }

        #endregion

    }
}
