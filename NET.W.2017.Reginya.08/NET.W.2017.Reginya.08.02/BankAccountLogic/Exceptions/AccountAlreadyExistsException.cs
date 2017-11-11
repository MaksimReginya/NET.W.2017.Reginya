using System;
using BankAccountLogic.AccountTypes;

namespace BankAccountLogic.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when account passed to create method already exists.
    /// </summary>
    public class AccountAlreadyExistsException : Exception
    {
        #region Public constructors

        /// <inheritdoc />
        /// <summary>
        ///  Initializes a new instance of <see cref="AccountAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="account">The account that already exists. </param>
        public AccountAlreadyExistsException(BankAccount account) : base($"Exception: the account {account} already exists.")
        {
            this.DuplicatingAccount = account;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The account that already exists.
        /// </summary>
        public BankAccount DuplicatingAccount { get; }

        #endregion        
    }
}
