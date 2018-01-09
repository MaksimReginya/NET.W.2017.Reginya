using System;
using BLL.Interface.Entities;

namespace BLL.Interface.ServiceInterface
{
    /// <summary>
    /// Interface of bank manage service.
    /// </summary>
    public interface IBankManageService
    {
        /// <summary>
        /// Registers user in bank.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password.</param>
        /// <param name="userFirstName">User's first name.</param>
        /// <param name="userLastName">User's last name.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred while registering new user.
        /// </exception>
        void RegisterUser(
            string email,
            string password,
            string userFirstName,
            string userLastName);

        /// <summary>
        /// Returns information about user with specified email.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <returns>Information about user.</returns>
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred while getting information.
        /// </exception>
        AccountOwner GetUserInfo(string email);

        /// <summary>
        /// Verifies if user is registered.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password</param>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred while verifying registration of the user.
        /// </exception>
        void VerifyRegistration(string email, string password);

        /// <summary>
        /// Creates a bank account for the specified user.
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <param name="balance">Balance of account.</param>
        /// <param name="bonus">Bonus on account.</param>
        /// <param name="type">Account type.</param>    
        /// <returns>Account's unique number.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred while creating account.
        /// </exception>
        string CreateAccount(
            string email,
            string password,
            AccountType type,
            decimal balance = 0m,
            int bonus = 0);

        /// <summary>
        /// Deposits money to the account with specific <see cref="accountNumber" />.
        /// </summary>
        /// <param name="email">Email of account owner.</param>
        /// <param name="accountNumber">Number of account to deposit.</param>
        /// <param name="value">Value to increase balance.</param> 
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>       
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred in deposit operation.
        /// </exception>
        void Deposit(string email, string accountNumber, decimal value);

        /// <summary>
        /// Withdraws money from the account with specific <see cref="accountNumber" />.
        /// </summary>
        /// <param name="email">Email of account owner.</param>
        /// <param name="accountNumber">Number of account to deposit.</param>
        /// <param name="value">Value to increase balance.</param> 
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>       
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred in withdraw operation.
        /// </exception>
        void Withdraw(string email, string accountNumber, decimal value);

        /// <summary>
        /// Transfers money from one account to another.
        /// </summary>
        /// <param name="fromEmail">source user's email.</param>
        /// <param name="fromAccountNumber">Source account number.</param>
        /// <param name="toEmail">Destination user's email.</param>
        /// <param name="toAccountNumber">Destination account number.</param>
        /// <param name="value">Transfer sum.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>       
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred in transfer operation.
        /// </exception>
        void MoneyTransfer(string fromEmail, string fromAccountNumber, string toEmail, string toAccountNumber, decimal value);

        /// <summary>
        /// Gets information about the account with specific <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to get info.</param>
        /// <returns>Information about the account.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred while getting account info.
        /// </exception>             
        string GetAccountInfo(string accountNumber);

        /// <summary>
        /// Closes the account with specified <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <param name="accountNumber">Number of account to close.</param>    
        /// <exception cref="ArgumentException">
        /// Thrown when one of the parameters is invalid.
        /// </exception>
        /// <exception cref="BankManageServiceException">
        /// Thrown when error occurred while closing the account.
        /// </exception>          
        void CloseAccount(string email, string password, string accountNumber);
    }
}
