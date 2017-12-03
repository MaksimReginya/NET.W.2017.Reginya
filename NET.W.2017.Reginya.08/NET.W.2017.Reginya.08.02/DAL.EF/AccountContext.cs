using System.Data.Entity;
using DAL.EF.Models;

namespace DAL.EF
{
    internal class AccountContext : DbContext
    {
        public AccountContext() : base("DefaultConnection")
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
