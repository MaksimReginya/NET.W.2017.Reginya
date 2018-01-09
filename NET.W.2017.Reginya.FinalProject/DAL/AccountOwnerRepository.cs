using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Mappers;
using ORM;
using ORM.Models;

namespace DAL
{
    /// <inheritdoc />    
    public class AccountOwnerRepository : IAccountOwnerRepository
    {
        #region Private fields
                
        private readonly DbContext _context;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">Database context with account owners.</param>
        public AccountOwnerRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public methods                

        /// <inheritdoc />    
        public void AddOwner(DtoAccountOwner owner)
        {
            if (owner is null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            try
            {
                var ormAccountOwner = _context.Set<AccountOwner>().FirstOrDefault(own => own.Email == owner.Email);
                if (!(ormAccountOwner is null))
                {
                    throw new RepositoryException("Owner with specified email already exists in repository.");
                }

                _context.Set<AccountOwner>().Add(owner.ToOrmAccountOwner());             
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in adding new account owner.", ex);
            }
        }

        /// <inheritdoc />
        public DtoAccountOwner GetOwnerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(nameof(email));
            }

            try
            {
                var ormAccountOwner = _context
                    .Set<AccountOwner>()
                    .Include(owner => owner.Accounts)
                    .FirstOrDefault(owner => owner.Email == email);
                return ormAccountOwner?.ToDtoAccountOwner();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in getting account owner by email.", ex);
            }            
        }

        /// <inheritdoc />    
        public void UpdateOwner(DtoAccountOwner owner)
        {
            if (owner is null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            try
            { 
                var updatingOwner = _context.Set<AccountOwner>().FirstOrDefault(own => own.Email == owner.Email);
                if (updatingOwner is null)
                {
                    throw new RepositoryException("Updating owner does not exist.");
                }

                UpdateOwner(updatingOwner, owner);
                _context.Entry(updatingOwner).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in updating account owner.", ex);
            }
        }
       
        /// <inheritdoc />    
        public IEnumerable<DtoAccountOwner> GetAllOwners()
        {
            try
            {
               return _context
                    .Set<AccountOwner>()
                    .Include(owner => owner.Accounts)
                    .ToList()
                    .Select(owner => owner.ToDtoAccountOwner()); 
            }            
            catch (Exception ex)
            {
                throw new RepositoryException("Error in getting all account owners.", ex);
            }            
        }

        #endregion

        #region Private methods

        private static void UpdateOwner(AccountOwner updatingOwner, DtoAccountOwner owner)
        {
            updatingOwner.Email = owner.Email;
            updatingOwner.FirstName = owner.FirstName;
            updatingOwner.LastName = owner.LastName;
            updatingOwner.Password = owner.Password;
        }

        #endregion
    }
}
