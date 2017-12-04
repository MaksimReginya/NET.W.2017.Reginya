using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Models
{
    /// <summary>
    /// Decorator of bank account entity that is used to be saved in repository.
    /// </summary>
    public class Account
    {
        #region Public properties

        /// <summary>
        /// Unique id of account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique account number.
        /// </summary>            
        public string AccountNumber { get; set; }
       
        /// <summary>
        /// Balance of the account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Bonus of the account.
        /// </summary>
        public int Bonus { get; set; }

        /// <summary>
        /// Id of owner of bank account.
        /// </summary>
        public int AccountOwnerId { get; set; }

        /// <summary>
        /// Id of type of bank account.
        /// </summary>
        public int AccountTypeId { get; set; }

        /// <summary>
        /// Owner of bank account.
        /// </summary>
        public virtual AccountOwner AccountOwner { get; set; }

        /// <summary>
        /// Type of bank account.
        /// </summary>
        public virtual AccountType AccountType { get; set; }

        #endregion       
    }
}
