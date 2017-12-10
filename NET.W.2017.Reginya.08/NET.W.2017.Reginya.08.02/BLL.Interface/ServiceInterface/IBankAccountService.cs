namespace BLL.Interface.ServiceInterface
{
    /// <summary>
    /// Interface of bank account service.
    /// </summary>
    public interface IBankAccountService
    {
        /// <summary>
        /// Creates the bank account.
        /// </summary>
        /// <param name="type">Account type.</param>        
        /// <param name="numberGenerator">Generator of unique account number.</param> 
        /// <param name="ownerFirstName">Owner's first name.</param>
        /// <param name="ownerLastName">Owner's last name.</param>
        /// <param name="balance">Balance of account.</param>
        /// <param name="bonus">Bonus on account.</param>
        /// <exception cref="BankAccountServiceException">
        /// Thrown when error occurred while creating new account.
        /// </exception>
        /// <returns>Account's unique number.</returns>                
        string CreateAccount(
            AccountType type,
            IAccountNumberGenerator numberGenerator,
            string ownerFirstName,
            string ownerLastName,
            decimal balance = 0m,
            int bonus = 0);

        /// <summary>
        /// Deposits money to the account with specific <see cref="accountNumber" />.
        /// </summary>
        /// <param name="accountNumber">Number of account to deposit.</param>
        /// <param name="value">Value to increase balance.</param>        
        /// <exception cref="BankAccountServiceException">
        /// Thrown when error occurred in deposit operation.
        /// </exception>
        void Deposit(string accountNumber, decimal value);

        /// <summary>
        /// Withdraws money from the account with specific <see cref="accountNumber" />.
        /// </summary>
        /// <param name="accountNumber">Number of account to withdraw.</param>
        /// <param name="value">Value to decrease balance.</param>
        /// <exception cref="BankAccountServiceException">
        /// Thrown when error occurred in withdraw operation.
        /// </exception>
        void Withdraw(string accountNumber, decimal value);

        /// <summary>
        /// Closes the account with specific <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to close.</param>    
        /// <exception cref="BankAccountServiceException">
        /// Thrown when error occurred while closing the account.
        /// </exception>    
        void CloseAccount(string accountNumber);

        /// <summary>
        /// Gets information about the account with specific <see cref="accountNumber"/>.
        /// </summary>
        /// <param name="accountNumber">Number of account to get info.</param>
        /// <exception cref="BankAccountServiceException">
        /// Thrown when error occurred while getting account info.
        /// </exception>
        /// <returns>Information about the account.</returns>        
        string GetAccountInfo(string accountNumber);
    }
}
