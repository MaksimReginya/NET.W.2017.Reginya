using System.Data.Entity;
using DAL.EF.Models;

namespace DAL.EF
{
    internal class AccountContext : DbContext
    {
        public AccountContext() : base("DefaultConnection")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountOwner> AccountOwners { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
    }
}
