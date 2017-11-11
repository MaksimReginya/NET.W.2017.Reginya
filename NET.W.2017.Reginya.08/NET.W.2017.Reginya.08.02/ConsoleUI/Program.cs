using System;
using System.Collections.Generic;
using BankAccountLogic.AccountTypes;
using BankAccountLogic.Service;
using Storage;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
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
            var bankAccountService = new BankAccountService(new BinaryFileStorage("accounts"));

            var accountList = new List<string>
            {
                bankAccountService.CreateAccount(typeof(BaseBankAccount), "Max", "Smith"),
                bankAccountService.CreateAccount(typeof(GoldBankAccount), "John", "Pitt"),
                bankAccountService.CreateAccount(typeof(PlatinumBankAccount), "Brad", "Fox", 500)
            };            
            PrintAccounts(bankAccountService, accountList);

            foreach (var accountNumber in accountList)
            {
                bankAccountService.Deposit(accountNumber, 15);                
            }

            PrintAccounts(bankAccountService, accountList);

            bankAccountService.CloseAccount(accountList[1]);
            accountList.RemoveAt(1);
            PrintAccounts(bankAccountService, accountList);

            foreach (var accountNumber in accountList)
            {
                bankAccountService.Withdraw(accountNumber, 5);
            }

            PrintAccounts(bankAccountService, accountList);            
        }
        
        private static void PrintAccounts(IService service, IEnumerable<string> accountNumbers)
        {
            foreach (var accountNumber in accountNumbers)
            {
                Console.WriteLine(service.GetAccountInfo(accountNumber));
            }

            Console.WriteLine();
        }
    }
}