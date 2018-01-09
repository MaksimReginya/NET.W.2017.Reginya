using System;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Web.Helpers;
using BLL.Interface.Entities;
using BLL.Interface.ServiceInterface;
using BLL.Mappers;
using DAL.Interface;

namespace BLL.ServiceImplementation
{
    /// <inheritdoc />
    /// <summary>
    /// Provides methods to work with bank accounts.
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Private fields        

        private readonly IBankAccountRepository _repository;
        private readonly IAccountNumberGenerator _numberGenerator;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">Account repository service.</param>                                
        /// /// <param name="numberGenerator">Generator of unique account number.</param>    
        public AccountService(IBankAccountRepository repository, IAccountNumberGenerator numberGenerator)
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (numberGenerator is null)
            {
                throw new ArgumentNullException(nameof(numberGenerator));
            }

            _repository = repository;
            _numberGenerator = numberGenerator;
        }

        #endregion

        #region IBankAccountService implementation

        /// <inheritdoc />
        public string CreateAccount(
            AccountOwner owner,
            AccountType type,            
            decimal balance = 0m,
            int bonus = 0)
        {            
            try
            {
                string accountNumber = _numberGenerator.CreateNumber(_repository.GetAllAccounts().ToBllAccounts());
                string cryptedAccountNumber = CryptoService.RijndaelEncrypt(accountNumber, owner.Email);
                var account = CreateAccountOfSpecifiedType(type, cryptedAccountNumber, owner, balance, bonus);
                _repository.AddAccount(account.ToDtoAccount());                
                return accountNumber;
            }
            catch (Exception ex)
            {
                throw new AccountServiceException("Some error occurred while creating new account.", ex);
            }            
        }

        /// <inheritdoc />
        public void Deposit(string accountNumber, decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Deposit value can't be negative.", nameof(value));
            }

            try
            {
                var dtoAccount = _repository.GetAccount(accountNumber);
                var account = dtoAccount.ToBllAccount(dtoAccount.AccountOwner.ToBllAccountOwner());
                account.Deposit(value);
                _repository.UpdateAccount(account.ToDtoAccount());                
            }
            catch (Exception ex)
            {
                throw new AccountServiceException("Some error occurred in deposit operation.", ex);
            }            
        }

        /// <inheritdoc />
        public void Withdraw(string accountNumber, decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Withdraw value can't be negative.", nameof(value));
            }

            try
            {                
                var dtoAccount = _repository.GetAccount(accountNumber);
                var account = dtoAccount.ToBllAccount(dtoAccount.AccountOwner.ToBllAccountOwner());
                account.Withdraw(value);
                _repository.UpdateAccount(account.ToDtoAccount());                
            }
            catch (Exception ex)
            {
                throw new AccountServiceException("Some error occurred in withdraw operation.", ex);
            }            
        }

        /// <inheritdoc />
        public void CloseAccount(string accountNumber)
        {
            try
            {
                _repository.RemoveAccount(_repository.GetAccount(accountNumber));                
            }
            catch (Exception ex)
            {
                throw new AccountServiceException("Some error occurred while closing account.", ex);
            }           
        }

        /// <inheritdoc />
        public string GetAccountInfo(string accountNumber)
        {
            try
            {
                var account = _repository.GetAccount(accountNumber);
                return account.ToBllAccount(account.AccountOwner.ToBllAccountOwner()).ToString();
            }
            catch (Exception ex)
            {
                throw new AccountServiceException("Some error occurred while getting account info.", ex);
            }                        
        }

        #endregion

        #region Private methods
       
        private static BankAccount CreateAccountOfSpecifiedType(
            AccountType type,
            string accountNumber,
            AccountOwner owner,
            decimal balance,
            int bonus)
        {
            switch (type)
            {
                case AccountType.Base:
                    return new BaseBankAccount(accountNumber, owner, balance, bonus);
                case AccountType.Gold:
                    return new GoldBankAccount(accountNumber, owner, balance, bonus);
                case AccountType.Platinum:
                    return new PlatinumBankAccount(accountNumber, owner, balance, bonus);
                default:
                    return null;
            }
        }       

        #endregion
    }
}
