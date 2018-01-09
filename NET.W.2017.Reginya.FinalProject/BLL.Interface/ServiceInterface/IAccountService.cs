using System;
using BLL.Interface.Entities;

namespace BLL.Interface.ServiceInterface
{
    /// <summary>
    /// Interface of account service.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Creates the bank account.
        /// </summary>
        /// <param name="type">Account type.</param>                
        /// <param name="owner">Owner of the account.</param>
        /// <param name="balance">Balance of account.</param>
        /// <param name="bonus">Bonus on account.</param>
        /// <exception cref="AccountServiceException">
        /// Thrown when error occurred while creating new account.
        /// </exception>
        /// <returns>Account's unique number.</returns>                
        string CreateAccount(
            AccountOwner owner,
            AccountType type,            
            decimal balance = 0m,
            int bonus = 0);

        /// <summary>
        /// Deposits money to the account with specified <see cref="accountNumber" />.
        /// </summary>
        /// <param name="accountNumber">Number of account to deposit.</param>
        /// <param name="value">Value to increase balance.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>         
        /// <exception cref="AccountServiceException">
        /// Thrown when error occurred in deposit operation.
        /// </exception>
        void Deposit(string accountNumber, decimal value);

        /// <summary>
        /// Withdraws money from the account with specified <see cref="accountNumber" />.
        /// </summary>
        /// <param name="accountNumber">Number of account to withdraw.</param>
        /// <param name="value">Value to decrease balance.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception> 
        /// <exception cref="AccountServiceException">
        /// Thrown when error occurred in withdraw operation.
        /// </exception>
        void Withdraw(string accountNumber, decimal value);

        /// <summary>
        /// Closes the account with specified <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to close.</param>    
        /// <exception cref="AccountServiceException">
        /// Thrown when error occurred while closing the account.
        /// </exception>    
        void CloseAccount(string accountNumber);

        /// <summary>
        /// Gets information about the account with specific <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to get info.</param>
        /// <exception cref="AccountServiceException">
        /// Thrown when error occurred while getting account info.
        /// </exception>
        /// <returns>Information about the account.</returns>        
        string GetAccountInfo(string accountNumber);
    }
}
