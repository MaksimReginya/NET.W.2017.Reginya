using BLL.Interface.Entities;
using BLL.Interface.ServiceInterface;
using PL.ASP_NET.ViewModels;

namespace PL.ASP_NET.Utils.Mappers
{
    public static class Mapper
    {
        public static AccountViewModel ToViewModel(this BankAccount account) =>
            new AccountViewModel
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Bonus = account.Bonus,
                Type = GetAccountType(account.AccountType)
            };

        private static AccountType GetAccountType(string type)
        {
            if (type.Contains("Gold"))
            {
                return AccountType.Gold;
            }

            if (type.Contains("Platinum"))
            {
                return AccountType.Platinum;
            }

            return AccountType.Base;
        }
    }
}