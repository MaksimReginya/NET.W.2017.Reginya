using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface
{
    /// <summary>
    /// Interface to work with accounts repository.
    /// </summary>
    public interface IBankAccountRepository
    {
        /// <summary>
        /// Adds account to the repository.
        /// </summary>
        /// <param name="account">Account to add.</param> 
        /// <exception cref="RepositoryException">
        /// Thrown if account already exists in repository.
        /// </exception>
        void AddAccount(DtoAccount account);

        /// <summary>
        /// Gets the account with specified number.
        /// </summary>
        /// <param name="accountNumber">Account's number.</param>
        /// <exception cref="RepositoryException">
        /// Thrown if account can't be found in repository.
        /// </exception>
        /// <returns>Requested account.</returns>
        DtoAccount GetAccount(string accountNumber);

        /// <summary>
        /// Updates account's information in the repository.
        /// </summary>
        /// <param name="account">Account to update.</param>      
        /// <exception cref="RepositoryException">
        /// Thrown if account can't be found in repository.
        /// </exception>  
        void UpdateAccount(DtoAccount account);

        /// <summary>
        /// Removes account from the repository.
        /// </summary>
        /// <param name="account">Account to remove.</param>       
        /// <exception cref="RepositoryException">
        /// Thrown if account can't be found in repository.
        /// </exception>  
        void RemoveAccount(DtoAccount account);

        /// <summary>
        /// Gets all accounts from the repository.
        /// </summary>         
        /// <returns>All accounts in the repository.</returns>        
        IEnumerable<DtoAccount> GetAllAccounts();
    }
}
