using System.Data.Entity;
using ORM.Models;

namespace ORM
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("AccountsDb")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<AccountOwner> AccountOwners { get; set; }

        public virtual DbSet<AccountType> AccountTypes { get; set; }
    }
}
