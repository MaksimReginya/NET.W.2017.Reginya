using System.Linq;
using DAL.Interface.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class AccountMappers
    {
        public static AccountOwner ToOrmAccountOwner(this DtoAccountOwner accountOwner) =>
            new AccountOwner
            {
                Email = accountOwner.Email,
                FirstName = accountOwner.FirstName,
                LastName = accountOwner.LastName,
                Password = accountOwner.Password
            };

        public static DtoAccountOwner ToDtoAccountOwner(this AccountOwner accountOwner)
        {
            var dtoOwner = new DtoAccountOwner
            {
                Email = accountOwner.Email,
                FirstName = accountOwner.FirstName,
                LastName = accountOwner.LastName,
                Password = accountOwner.Password                
            };

            dtoOwner.Accounts.AddRange(accountOwner.Accounts.Select(account => account.ToDtoAccount(dtoOwner)));
            return dtoOwner;
        }

        public static Account ToOrmAccount(this DtoAccount account, AccountOwner accountOwner) =>
            new Account
            {
                AccountType = new AccountType
                {
                    Name = account.AccountType
                },
                Balance = account.Balance,
                Bonus = account.Bonus,
                AccountNumber = account.AccountNumber,
                AccountOwner = accountOwner
            };

        public static DtoAccount ToDtoAccount(this Account account, DtoAccountOwner accountOwner) =>
            new DtoAccount
            {
                AccountType = account.AccountType.Name,
                Balance = account.Balance,
                Bonus = account.Bonus,
                AccountNumber = account.AccountNumber,
                AccountOwner = accountOwner
            };
    }
}
