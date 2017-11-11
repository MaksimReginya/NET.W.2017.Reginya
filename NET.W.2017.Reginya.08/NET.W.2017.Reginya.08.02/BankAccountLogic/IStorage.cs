using System.Collections.Generic;
using BankAccountLogic.AccountTypes;

namespace BankAccountLogic
{
    /// <summary>
    /// Interface to work with accounts storage.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Adds account to the storage.
        /// </summary>
        /// <param name="account">Account to add</param>        
        void AddAccount(BankAccount account);

        /// <summary>
        /// Gets the account with specified number.
        /// </summary>
        /// <param name="accountNumber">Account's number</param>
        /// <returns>Requested account.</returns>
        BankAccount GetAccount(string accountNumber);

        /// <summary>
        /// Updates account's information in the storage.
        /// </summary>
        /// <param name="account">Account to update</param>        
        void UpdateAccount(BankAccount account);

        /// <summary>
        /// Removes account from the storage.
        /// </summary>
        /// <param name="account">Account to remove</param>       
        void RemoveAccount(BankAccount account);

        /// <summary>
        /// Gets all accounts that are in the storage.
        /// </summary>
        /// <returns>All accounts in the storage.</returns>        
        IEnumerable<BankAccount> GetAllAccounts();
    }
}
