using System;

namespace BankAccountLogic.Service
{
    /// <summary>
    /// Interface of bank account service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Creates the bank account.
        /// </summary>
        /// <param name="type">Account type</param>        
        /// <param name="ownerFirstName">Owner's first name</param>
        /// <param name="ownerLastName">Owner's last name</param>
        /// <param name="balance">Balance of account</param>
        /// <param name="bonus">Bonus on account</param>
        /// <returns>Account's unique number.</returns>        
        string CreateAccount(Type type, string ownerFirstName, string ownerLastName, decimal balance = 0m, int bonus = 0);

        /// <summary>
        /// Deposits money to the account with specific <see cref="accountNumber" />.
        /// </summary>
        /// <param name="accountNumber">Number of account to deposit</param>
        /// <param name="value">Value to increase balance</param>        
        void Deposit(string accountNumber, decimal value);

        /// <summary>
        /// Withdraws money from the account with specific <see cref="accountNumber" />.
        /// </summary>
        /// <param name="accountNumber">Number of account to withdraw</param>
        /// <param name="value">Value to decrease balance</param>
        void Withdraw(string accountNumber, decimal value);

        /// <summary>
        /// Closes the account with specific <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to close</param>        
        void CloseAccount(string accountNumber);

        /// <summary>
        /// Gets information about the account with specific <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to get info</param>
        /// <returns>Information about the account</returns>        
        string GetAccountInfo(string accountNumber);
    }
}
