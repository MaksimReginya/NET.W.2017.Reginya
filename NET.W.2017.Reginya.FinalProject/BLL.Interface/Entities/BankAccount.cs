using System;
using System.Text.RegularExpressions;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// Base class for all types of bank accounts
    /// </summary>
    public abstract class BankAccount : IEquatable<BankAccount>
    {
        #region Private fields

        private string _accountNumber;
        private AccountOwner _owner;
        private decimal _balance;
        private int _bonus;

        #endregion                                

        #region Protected constructors

        /// <summary>        
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">Unique account number.</param>
        /// <param name="owner">Owner of the account.</param>
        /// <param name="balance">Balance of account.</param>
        /// <param name="bonus">Bonus on account.</param>
        protected BankAccount(string accountNumber, AccountOwner owner, decimal balance = 0m, int bonus = 0)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = balance;
            Bonus = bonus;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Unique account number.
        /// </summary>
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (!IsAccountNumberValid(value))
                {
                    throw new ArgumentException("Account number is not valid.", nameof(AccountNumber));
                }

                _accountNumber = value;
            }
        }
                               
        /// <summary>
        /// Owner of the account.
        /// </summary>
        public AccountOwner Owner
        {
            get => _owner;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Owner), $"{nameof(Owner)} can't be null.");
                }

                _owner = value;
            }
        }

        /// <summary>
        /// Balance of the account.
        /// </summary>
        public decimal Balance
        {
            get => _balance;
            private set
            {
                if (value < MinBalance)
                {
                    throw new ArgumentException($"{Balance} can must be bigger than {MinBalance}.");
                }

                _balance = value;
            }
        }

        /// <summary>
        /// Bonus of the account.
        /// </summary>
        public int Bonus
        {
            get => _bonus;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{Bonus} can not be negative.");
                }

                _bonus = value;
            }
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// Minimal balance of the account.
        /// </summary>
        protected abstract decimal MinBalance { get; }

        /// <summary>
        /// Balance "cost" for bonus calculation .
        /// </summary>
        protected abstract int BalanceCost { get; }
        
        /// <summary>
        /// Transaction "cost" for bonus calculation.
        /// </summary>
        protected abstract int TransactionCost { get; }

        #endregion
        
        #region Public methods
       
        /// <summary>
        /// Determines if value is valid account number.
        /// </summary>
        /// <param name="value">Number to check.</param>
        /// <returns>
        /// True if number is valid, false - if not.
        /// </returns>
        public virtual bool IsAccountNumberValid(string value)
        {
            if (value?.Length != 20)
            {
                return false;
            }

            return new Regex(@"[0-9]{5}[a-z]{4}[0-9]{11}").IsMatch(value);
        }

        /// <summary>
        /// Performs decreasing of account balance on value.
        /// </summary>
        /// <param name="value">Value to decrease balance.</param>
        public void Withdraw(decimal value)
        {
            if (Balance - value < MinBalance)
            {
                throw new ArgumentException("Withdraw value is too big.");
            }

            Balance -= value;

            Bonus -= CalculateBonus(value) / 2;

            Bonus = Bonus < 0 ? 0 : Bonus;
        }

        /// <summary>
        /// Performs increasing of account balance on value.
        /// </summary>
        /// <param name="value">Value to increase balance.</param>
        public void Deposit(decimal value)
        {
            if (value >= decimal.MaxValue - Balance)
            {
                throw new ArgumentException("Deposit value is too big.");
            }

            Balance += value;

            int bonus = CalculateBonus(value);
            Bonus = (Bonus >= int.MaxValue - bonus) ? int.MaxValue : Bonus + bonus;
        }

        /// <summary>
        /// Calculates bonus of account.
        /// </summary>        
        /// <param name="transactionValue">Transaction value.</param>                
        /// <returns></returns>
        public abstract int CalculateBonus(decimal transactionValue);

        #endregion

        #region Overloaded methods of class Object 

        /// <summary>
        /// Determines whether the specified instance of <see cref="BankAccount"/>
        /// class is equal to the current instance.
        /// </summary>
        /// <param name="other">Bank account to compare with the current instance.</param>
        /// <returns>Result of comparison.</returns>
        public override bool Equals(object other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other.GetType() != this.GetType() && this.Equals((BankAccount)other);
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="BankAccount"/> class.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode() => AccountNumber.GetHashCode();

        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/>.
        /// </summary>
        /// <returns>String representation of bank account.</returns>
        public override string ToString()
        {
            return $"Owner's info: {Owner}, Account number: {AccountNumber}, Balance: {Balance}, Bonus: {Bonus}.";
        }

        #endregion

        #region IEquatable<BankAccount> implementation

        /// <inheritdoc />
        /// <summary>
        /// Compares two accounts on equality.
        /// </summary>
        /// <param name="other">Bank account to compare with the current instance.</param>  
        /// <returns>Result of comparison.</returns>
        public bool Equals(BankAccount other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return AccountNumber.Equals(other.AccountNumber);
        }

        #endregion
    }
}
