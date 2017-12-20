using System;
using System.Collections.Generic;
using BLL.Interface.ServiceInterface;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    internal class Program
    {
        private static readonly IAccountNumberGenerator NumberGenerator;
        private static readonly IKernel Kernel;

        static Program()
        {
            Kernel = new StandardKernel();
            Kernel.ConfigurateResolverConsole();
            NumberGenerator = Kernel.Get<IAccountNumberGenerator>();            
        }

        private static void Main()
        {
            try
            {                
                var accountService = Kernel.Get<IBankAccountService>();                
                BankAccountServiceTest(accountService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        
        private static void BankAccountServiceTest(IBankAccountService bankAccountService)
        {            
            var accountList = new List<string>
            {
                bankAccountService.CreateAccount(AccountType.Base, NumberGenerator, "Max", "Smith", "example1@mail.ru"),
                bankAccountService.CreateAccount(AccountType.Gold, NumberGenerator, "John", "Pitt", "example1@mail.ru"),
                bankAccountService.CreateAccount(AccountType.Platinum, NumberGenerator, "Brad", "Fox", "example1@mail.ru", 500)
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

        private static void PrintAccounts(IBankAccountService bankAccountService, IEnumerable<string> accountNumbers)
        {
            foreach (var accountNumber in accountNumbers)
            {
                Console.WriteLine(bankAccountService.GetAccountInfo(accountNumber));
            }

            Console.WriteLine();
        }
    }
}
