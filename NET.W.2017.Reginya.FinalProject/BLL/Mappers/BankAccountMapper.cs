using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.ServiceImplementation;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    /// <summary>
    /// Maps business logic entities to data access layer entities and vise versa.
    /// </summary>
    public static class BankAccountMapper
    {
        #region Public methods

        public static DtoAccountOwner ToDtoAccountOwner(this AccountOwner owner, string password) =>
            new DtoAccountOwner
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                Password = password
            };

        public static AccountOwner ToBllAccountOwner(this DtoAccountOwner owner)
        {
            var result = new AccountOwner(owner.FirstName, owner.LastName, owner.Email);
            result.Accounts.AddRange(owner.Accounts.Select(account => account.ToBllAccount(result)));
            return result;
        }

        /// <summary>
        /// Maps <paramref cref="BankAccount"/> to <paramref cref="DtoAccount"/>.
        /// </summary>
        public static DtoAccount ToDtoAccount(this BankAccount account) =>
            new DtoAccount
            {
                AccountType = account.GetType().AssemblyQualifiedName,
                AccountNumber = account.AccountNumber,
                AccountOwner = account.Owner.ToDtoAccountOwner(string.Empty),
                Balance = account.Balance,
                Bonus = account.Bonus
            };

        /// <summary>
        /// Maps <paramref cref="DtoAccount"/> to <paramref cref="BankAccount"/>.
        /// </summary>
        public static BankAccount ToBllAccount(this DtoAccount account, AccountOwner owner)
        {
            var type = GetBllAccountType(account.AccountType);
            var bllAccount = (BankAccount) Activator.CreateInstance(
                type,
                CryptoService.RijndaelDecrypt(account.AccountNumber, owner.Email),
                owner,
                account.Balance,
                account.Bonus);
            bllAccount.AccountType = type.Name;
            return bllAccount;
        }

        /// <summary>
        /// Maps enumerable of <paramref cref="DtoAccount"/> to enumerable of <paramref cref="BankAccount "/>.
        /// </summary>
        public static IEnumerable<BankAccount> ToBllAccounts(this IEnumerable<DtoAccount> accounts)
            => new List<BankAccount>(accounts.Select(account =>
                account.ToBllAccount(account.AccountOwner.ToBllAccountOwner())));
            /*(BankAccount)Activator.CreateInstance(
                GetBllAccountType(account.AccountType),
                account.AccountNumber,
                account.AccountOwner.ToBllAccountOwner(),
                account.Balance,
                account.Bonus)));*/

        #endregion

        #region Private methods
                
        private static Type GetBllAccountType(string type)
        {
            if (type.Contains("Gold"))
            {
                return typeof(GoldBankAccount);
            }

            if (type.Contains("Platinum"))
            {
                return typeof(PlatinumBankAccount);
            }

            return typeof(BaseBankAccount);
        }

        #endregion
    }
}
