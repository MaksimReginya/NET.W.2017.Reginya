using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface
{
    /// <summary>
    /// Interface to work with account owners repository.
    /// </summary>
    public interface IAccountOwnerRepository
    {
        /// <summary>
        /// Adds account owner to the repository.
        /// </summary>
        /// <param name="owner">Owner to add.</param> 
        /// <exception cref="RepositoryException">
        /// Thrown if account owner already exists in repository.
        /// </exception>
        void AddOwner(DtoAccountOwner owner);

        /// <summary>
        /// Gets the account owner with specified email.
        /// </summary>
        /// <param name="email">Owner's email.</param>                        
        /// <returns>Requested account owner or null if owner can't be found.</returns>
        DtoAccountOwner GetOwnerByEmail(string email);

        /// <summary>
        /// Updates account owner's information in the repository.
        /// </summary>
        /// <param name="owner">Account owner to update.</param>      
        /// <exception cref="RepositoryException">
        /// Thrown if account owner can't be found in repository.
        /// </exception>  
        void UpdateOwner(DtoAccountOwner owner);
       
        /// <summary>
        /// Gets all account owners from the repository.
        /// </summary>         
        /// <returns>All account owners in the repository.</returns>        
        IEnumerable<DtoAccountOwner> GetAllOwners();
    }
}
