using System;

namespace BankAccountLogic.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when account passed to close method can't be found.
    /// </summary>
    public class AccountNotFoundException : Exception
    {                                             
        #region Public constructors

        /// <inheritdoc />
        /// <summary>
        ///  Initializes a new instance of <see cref="AccountNotFoundException"/> class.
        /// </summary>
        /// <param name="accountNumber">The account number that can't be found. </param>
        public AccountNotFoundException(string accountNumber)
            : base($"Exception: the account with number: {accountNumber} can't be found.")
        {            
        }

        #endregion
    }
}
