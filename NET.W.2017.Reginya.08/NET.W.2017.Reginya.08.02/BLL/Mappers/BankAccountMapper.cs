using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    /// <summary>
    /// Maps business logic entities to data access layer entities and vise versa.
    /// </summary>
    public static class BankAccountMapper
    {
        /// <summary>
        /// Maps <paramref cref="BankAccount"/> to <paramref cref="DalAccount"/>.
        /// </summary>
        public static DalAccount ToDalAccount(this BankAccount account) =>
            new DalAccount
            {
                AccountType = account.GetType(),
                AccountNumber = account.AccountNumber,
                OwnerFirstName = account.OwnerFirstName,
                OwnerLastName = account.OwnerLastName,
                Balance = account.Balance,
                Bonus = account.Bonus
            };

        /// <summary>
        /// Maps <paramref cref="DalAccount"/> to <paramref cref="BankAccount"/>.
        /// </summary>
        public static BankAccount ToBllAccount(this DalAccount dalAccount) =>
            (BankAccount)Activator.CreateInstance(
                dalAccount.AccountType,
                dalAccount.AccountNumber,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerLastName,
                dalAccount.Balance,
                dalAccount.Bonus);

        /// <summary>
        /// Maps enumerable of <paramref cref="BankAccount"/> to enumerable of <paramref cref="DalAccount "/>.
        /// </summary>
        public static IEnumerable<DalAccount> ToDalAccounts(this IEnumerable<BankAccount> accounts)
            => new List<DalAccount>(accounts.Select(account => new DalAccount
            {
                AccountType = account.GetType(),
                AccountNumber = account.AccountNumber,
                OwnerFirstName = account.OwnerFirstName,
                OwnerLastName = account.OwnerLastName,
                Balance = account.Balance,
                Bonus = account.Bonus
            }));

        /// <summary>
        /// Maps enumerable of <paramref cref="DalAccount"/> to enumerable of <paramref cref="BankAccount "/>.
        /// </summary>
        public static IEnumerable<BankAccount> ToBllAccounts(this IEnumerable<DalAccount> accounts)
            => new List<BankAccount>(accounts.Select(account => (BankAccount)Activator.CreateInstance(
                account.AccountType,
                account.AccountNumber,
                account.OwnerFirstName,
                account.OwnerLastName,
                account.Balance,
                account.Bonus)));
    }
}
