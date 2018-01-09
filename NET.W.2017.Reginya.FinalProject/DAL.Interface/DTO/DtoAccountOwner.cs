using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DtoAccountOwner
    {
        /// <summary>
        /// Initializes a new instance of the<see cref="DtoAccountOwner"/> class.
        /// </summary>        
        public DtoAccountOwner()
        {
            Accounts = new List<DtoAccount>();
        }
        
        /// <summary>
        /// Owner's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Owner's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Owner's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Owner's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// List of owner's accounts.
        /// </summary>
        public List<DtoAccount> Accounts { get; set; }
    }
}
