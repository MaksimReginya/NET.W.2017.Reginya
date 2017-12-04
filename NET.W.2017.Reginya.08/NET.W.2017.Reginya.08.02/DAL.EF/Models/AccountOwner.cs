using System.Collections.Generic;

namespace DAL.EF.Models
{
    public class AccountOwner
    {
        /// <summary>
        /// Unique id of account owner.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Owner's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Owner's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Collection of owner's accounts.
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
