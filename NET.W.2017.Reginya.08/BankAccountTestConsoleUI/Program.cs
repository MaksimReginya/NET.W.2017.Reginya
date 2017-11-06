using System;
using System.Collections.Generic;
using System.IO;

using AccountsStorage;
using BankAccountLogic;
using BankAccountLogic.AccountTypes;
using BankAccountLogic.Predicates;

namespace BankAccountTestConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (File.Exists("accounts"))
                    LoadFromStorageTest("accounts");
                else
                    BankAccountsServiceTest();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void BankAccountsServiceTest()
        {
            var accounts = new List<BankAccount>
            {
                new BaseBankAccount("12345abcd12345678900", "Max", "Smith"),
                new GoldBankAccount("12345abcd12345678901", "John", "Pitt")
            };

            var bookListService = new BankAccountsService(accounts);
            PrintAccounts(bookListService.GetAccounts());

            bookListService.Create(new PlatinumBankAccount("12345abcd12345678902", "Brad", "Fox", 500));
            PrintAccounts(bookListService.GetAccounts());


            bookListService.FindAccountByTag(new IsNumberValid("12345abcd12345678900")).Replenish(12);
            bookListService.FindAccountByTag(new IsNumberValid("12345abcd12345678901")).Replenish(12);
            PrintAccounts(bookListService.GetAccounts());

            bookListService.FindAccountByTag(new IsNumberValid("12345abcd12345678900")).Withdraw(7);
            bookListService.FindAccountByTag(new IsNumberValid("12345abcd12345678901")).Withdraw(7);
            PrintAccounts(bookListService.GetAccounts());

            bookListService.Remove("12345abcd12345678901");
            PrintAccounts(bookListService.GetAccounts());                        

            bookListService.Save(new BinaryFileStorage("accounts"));
        }

        private static void LoadFromStorageTest(string storageName)
        {
            var bookListService = new BankAccountsService();
            bookListService.Load(new BinaryFileStorage(storageName));

            PrintAccounts(bookListService.GetAccounts());
        }

        private static void PrintAccounts(IEnumerable<BankAccount> accounts)
        {
            foreach (var account in accounts)
                Console.WriteLine(account);
            Console.WriteLine();
        }
    }
}