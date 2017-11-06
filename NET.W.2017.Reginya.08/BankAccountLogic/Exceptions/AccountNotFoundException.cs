using System;

using BankAccountLogic.AccountTypes;

namespace BankAccountLogic.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when account passed to remove method can't be found in list.
    /// </summary>
    internal class AccountNotFoundException : Exception
    {                                             
        #region Public constructors

        /// <inheritdoc />
        /// <summary>
        ///  Initializes a new instance of <see cref="AccountNotFoundException"/> class.
        /// </summary>
        /// <param name="accountNumber">The account number that can't be found in list. </param>
        public AccountNotFoundException(string accountNumber) : base(
            $"Exception: the account with number: {accountNumber} can't be found in list.")
        {}

        #endregion
    }
}
