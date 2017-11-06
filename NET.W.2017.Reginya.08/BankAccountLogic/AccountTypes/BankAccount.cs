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
        private decimal _balance;
        private decimal _bonus;

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
        public decimal Balance
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
        public decimal Bonus
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

        #region Protected properties

        /// <summary>
        /// Balance "cost" for bonus calculation 
        /// </summary>
        protected abstract int BalanceCost { get; }
        /// <summary>
        /// Transaction "cost" for bonus calculation
        /// </summary>
        protected abstract int TransactionCost { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Compares two accounts on equality
        /// </summary>
        /// <param name="other">Bank account to compare with the current instance</param>  
        /// <returns>Result of comparasion</returns>
        public bool Equals(BankAccount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return AccountNumber.Equals(other.AccountNumber) && OwnerFirstName.Equals(other.OwnerFirstName) &&
                   OwnerLastName.Equals(other.OwnerLastName) && Balance.Equals(other.Balance) &&
                   Bonus.Equals(other.Bonus);
        }

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
        public void Withdraw(decimal value)
        {
            if (Balance - value < 0)
                throw new ArgumentException("Withdraw value can't be bigger than balance.");

            Balance -= value;

            Bonus -= BonusLogic.CalculateBonus(Balance, value, BalanceCost, TransactionCost) / 2;

            if (Bonus < 0)
                Bonus = 0m;
        }

        /// <summary>
        /// Performs increasing of account balance on value
        /// </summary>
        /// <param name="value">Value to increase balance</param>
        public void Replenish(decimal value)
        {
            if (value >= decimal.MaxValue - Balance)
                throw new ArgumentException("Value to increase balance is too big.");

            Balance += value;

            decimal bonus = BonusLogic.CalculateBonus(Balance, value, BalanceCost, TransactionCost);
            if (Bonus >= decimal.MaxValue - bonus)
                Bonus = decimal.MaxValue;
            else
                Bonus += bonus;
        }

        #endregion

        #region Overloaded methods of class Object 

        /// <summary>
        /// Determines whether the specified instance of <see cref="BankAccount"/>
        /// class is equal to the current instance
        /// </summary>
        /// <param name="other">Bank account to compare with the current instance</param>
        /// <returns>Result of comparasion</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            return Equals((BankAccount)other);
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="BankAccount"/> class
        /// </summary>
        /// <returns>The hash code for this instance</returns>
        public override int GetHashCode() => AccountNumber.GetHashCode();

        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/>
        /// </summary>
        /// <returns>String representation of bank account</returns>
        public override string ToString()
        {
            return $"Account number: {AccountNumber}, Owner's first name: {OwnerFirstName}," +
                   $" Owner's last name: {OwnerLastName}, Balance: {Balance}, Bonus: {Bonus}.";
        }

        #endregion
    }
}
