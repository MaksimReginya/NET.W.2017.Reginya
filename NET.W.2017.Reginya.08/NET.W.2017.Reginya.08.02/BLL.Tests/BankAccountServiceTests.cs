using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.ServiceInterface;
using BLL.ServiceImplementation;
using DAL.Interface;
using DAL.Interface.DTO;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class BankAccountServiceTests
    {        
        [TestCase("Max", "Smith", "00000aaaa00000000000")]
        [TestCase("John", "Pitt", "00000aaaa00000000000")]
        public void AccountNumberGenerator_CreateNumberTest(string firstName, string lastName, string expectedAccountNumber)
        {            
            var repositoryMock = new Mock<IBankAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>(MockBehavior.Strict);

            accountNumberGeneratorMock.Setup(service => service.CreateNumber(new List<BankAccount>())).Returns(expectedAccountNumber);
            var bankAccountService = new BankAccountService(repositoryMock.Object, unitOfWorkMock.Object);
            
            string actualAccountNumber = bankAccountService.CreateAccount(
                AccountType.Base,
                accountNumberGeneratorMock.Object,
                firstName,
                lastName);
            
            Assert.AreEqual(expectedAccountNumber, actualAccountNumber);
        }

        [TestCase("Max", "Smith", "00000aaaa00000000000")]
        [TestCase("John", "Pitt", "00000aaaa00000000000")]
        public void BankAccountRepository_GetAccountTest(string firstName, string lastName, string expectedAccountNumber)
        {
            var repositoryMock = new Mock<IBankAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>(MockBehavior.Strict);

            accountNumberGeneratorMock.Setup(service => service.CreateNumber(new List<BankAccount>())).Returns(expectedAccountNumber);
            var bankAccountService = new BankAccountService(repositoryMock.Object, unitOfWorkMock.Object);

            bankAccountService.CreateAccount(
                AccountType.Base,
                accountNumberGeneratorMock.Object,
                firstName,
                lastName);

            repositoryMock.Verify(repository => repository.GetAllAccounts(), Times.Once);
        }

        [TestCase("Max", "Smith", "00000aaaa00000000000")]
        [TestCase("John", "Pitt", "00000aaaa00000000000")]
        public void BankAccountRepository_GetAllAccountsTest(string firstName, string lastName, string expectedAccountNumber)
        {
            var repositoryMock = new Mock<IBankAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>(MockBehavior.Strict);

            accountNumberGeneratorMock.Setup(service => service.CreateNumber(new List<BankAccount>())).Returns(expectedAccountNumber);
            var bankAccountService = new BankAccountService(repositoryMock.Object, unitOfWorkMock.Object);
            
            bankAccountService.CreateAccount(
                AccountType.Base,
                accountNumberGeneratorMock.Object,
                firstName,
                lastName);
            
            repositoryMock.Verify(repository => repository.GetAllAccounts(), Times.Once);
        }

        [TestCase("Max", "Smith", "00000aaaa00000000000")]
        [TestCase("John", "Pitt", "00000aaaa00000000000")]
        public void BankAccountRepository_AddAccountTest(string firstName, string lastName, string expectedAccountNumber)
        {
            var repositoryMock = new Mock<IBankAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>(MockBehavior.Strict);

            accountNumberGeneratorMock.Setup(service => service.CreateNumber(new List<BankAccount>())).Returns(expectedAccountNumber);
            var bankAccountService = new BankAccountService(repositoryMock.Object, unitOfWorkMock.Object);

            bankAccountService.CreateAccount(
                AccountType.Base,
                accountNumberGeneratorMock.Object,
                firstName,
                lastName);                        
         
            repositoryMock.Verify(
                repository => repository.AddAccount(It.Is<DtoAccount>(account => account.AccountNumber == expectedAccountNumber)),
                Times.Once);
        }

        [TestCase("Max", "Smith", "00000aaaa00000000000")]
        [TestCase("John", "Pitt", "00000aaaa00000000000")]
        public void BankAccountRepository_UpdateAccountTest(string firstName, string lastName, string expectedAccountNumber)
        {
            var repositoryMock = new Mock<IBankAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            repositoryMock.Setup(repository => repository.GetAccount(It.IsAny<string>())).Returns(
                new DtoAccount
                {
                    AccountNumber = expectedAccountNumber,
                    AccountType = "BaseBankAccount",
                    Balance = 100,
                    Bonus = 100,
                    OwnerFirstName = firstName,
                    OwnerLastName = lastName
                });
            var accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>(MockBehavior.Strict);

            accountNumberGeneratorMock.Setup(service => service.CreateNumber(new List<BankAccount>())).Returns(expectedAccountNumber);
            var bankAccountService = new BankAccountService(repositoryMock.Object, unitOfWorkMock.Object);

            string actualAccountNumber = bankAccountService.CreateAccount(
                AccountType.Base,
                accountNumberGeneratorMock.Object,
                firstName,
                lastName);
            
            bankAccountService.Deposit(actualAccountNumber, 100m);
            bankAccountService.Withdraw(actualAccountNumber, 10m);
          
            repositoryMock.Verify(
                repository => repository.UpdateAccount(It.Is<DtoAccount>(account => account.AccountNumber == expectedAccountNumber)),
                Times.Exactly(2));                                 
        }

        [TestCase("Max", "Smith", "00000aaaa00000000000")]
        [TestCase("John", "Pitt", "00000aaaa00000000000")]
        public void BankAccountRepository_RemoveAccountTest(string firstName, string lastName, string expectedAccountNumber)
        {
            var repositoryMock = new Mock<IBankAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            repositoryMock.Setup(repository => repository.GetAccount(It.IsAny<string>())).Returns(
                new DtoAccount
                {
                    AccountNumber = expectedAccountNumber,
                    AccountType = "BaseBankAccount",
                    Balance = 0,
                    Bonus = 0,
                    OwnerFirstName = firstName,
                    OwnerLastName = lastName
                });
            var accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>(MockBehavior.Strict);

            accountNumberGeneratorMock.Setup(service => service.CreateNumber(new List<BankAccount>())).Returns(expectedAccountNumber);
            var bankAccountService = new BankAccountService(repositoryMock.Object, unitOfWorkMock.Object);

            bankAccountService.CreateAccount(
                AccountType.Base,
                accountNumberGeneratorMock.Object,
                firstName,
                lastName);

            bankAccountService.CloseAccount(expectedAccountNumber);
                               
            repositoryMock.Verify(
                repository => repository.RemoveAccount(It.Is<DtoAccount>(account => account.AccountNumber == expectedAccountNumber)),
                Times.Once);
        }
    }
}
