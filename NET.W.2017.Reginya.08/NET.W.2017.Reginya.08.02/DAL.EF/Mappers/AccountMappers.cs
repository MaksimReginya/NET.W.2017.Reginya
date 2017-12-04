using DAL.EF.Models;
using DAL.Interface.DTO;

namespace DAL.EF.Mappers
{
    public static class AccountMappers
    {
        public static Account ToOrmAccount(this DtoAccount account) =>
            new Account
            {
                AccountType = new AccountType
                {
                    Name = account.AccountType
                },
                Balance = account.Balance,
                Bonus = account.Bonus,
                AccountNumber = account.AccountNumber,
                AccountOwner = new AccountOwner
                {
                    FirstName = account.OwnerFirstName,
                    LastName = account.OwnerLastName
                }
            };

        public static DtoAccount ToDtoAccount(this Account account) =>
            new DtoAccount
            {
                AccountType = account.AccountType.Name,
                Balance = account.Balance,
                Bonus = account.Bonus,
                AccountNumber = account.AccountNumber,
                OwnerFirstName = account.AccountOwner.FirstName,
                OwnerLastName = account.AccountOwner.LastName
            };
    }
}
