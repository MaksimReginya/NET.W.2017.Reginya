using System.Collections.Generic;

namespace DAL.EF.Models
{
    public class AccountType
    {
        /// <summary>
        /// Unique id of account type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the type of account.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Collection of accounts of current type.
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
