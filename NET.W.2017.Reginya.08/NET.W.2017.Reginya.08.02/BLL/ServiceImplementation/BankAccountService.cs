﻿using System;
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
    public class BankAccountService : IBankAccountService
    {
        #region Private fields        

        private readonly IBankAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountService"/> class.
        /// </summary>
        /// <param name="repository">Account repository service.</param>                        
        /// <param name="unitOfWork">Perfoms confirmation of changes in repository.</param>  
        public BankAccountService(IBankAccountRepository repository, IUnitOfWork unitOfWork)
        {
            if (ReferenceEquals(repository, null))
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (ReferenceEquals(unitOfWork, null))
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IBankAccountService implementation

        /// <inheritdoc />
        public string CreateAccount(
            AccountType type,
            IAccountNumberGenerator numberGenerator,
            string ownerFirstName,
            string ownerLastName,
            decimal balance = 0m,
            int bonus = 0)
        {
            if (ReferenceEquals(numberGenerator, null))
            {
                throw new ArgumentNullException(nameof(numberGenerator));
            }

            try
            {
                string accountNumber = numberGenerator.CreateNumber(_repository.GetAllAccounts().ToBllAccounts());
                var account = CreateAccountOfSpecifiedType(type, accountNumber, ownerFirstName, ownerLastName, balance, bonus);

                _repository.AddAccount(account.ToDtoAccount());
                _unitOfWork.Commit();

                return accountNumber;
            }
            catch (Exception ex)
            {
                throw new BankAccountServiceException("Some error occuried while creating new account.", ex);
            }            
        }

        /// <inheritdoc />
        public void Deposit(string accountNumber, decimal value)
        {
            try
            {
                var account = _repository.GetAccount(accountNumber).ToBllAccount();
                account.Deposit(value);
                _repository.UpdateAccount(account.ToDtoAccount());
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankAccountServiceException("Some error occuried in deposit operation.", ex);
            }            
        }

        /// <inheritdoc />
        public void Withdraw(string accountNumber, decimal value)
        {
            try
            {                
                var account = _repository.GetAccount(accountNumber).ToBllAccount();
                account.Withdraw(value);
                _repository.UpdateAccount(account.ToDtoAccount());
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankAccountServiceException("Some error occuried in withdraw operation.", ex);
            }            
        }

        /// <inheritdoc />
        public void CloseAccount(string accountNumber)
        {
            try
            {
                _repository.RemoveAccount(_repository.GetAccount(accountNumber));
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankAccountServiceException("Some error occuried while closing account.", ex);
            }           
        }

        /// <inheritdoc />
        public string GetAccountInfo(string accountNumber)
        {
            try
            {
                return _repository.GetAccount(accountNumber).ToBllAccount().ToString();
            }
            catch (Exception ex)
            {
                throw new BankAccountServiceException("Some error occuried while getting account info.", ex);
            }                        
        }

        #endregion

        #region Private methods
       
        private static BankAccount CreateAccountOfSpecifiedType(
            AccountType type, string accountNumber, string ownerFirstName, string ownerLastName, decimal balance, int bonus)
        {
            switch (type)
            {
                case AccountType.Base:
                    return new BaseBankAccount(accountNumber, ownerFirstName, ownerLastName, balance, bonus);
                case AccountType.Gold:
                    return new GoldBankAccount(accountNumber, ownerFirstName, ownerLastName, balance, bonus);
                case AccountType.Platinum:
                    return new PlatinumBankAccount(accountNumber, ownerFirstName, ownerLastName, balance, bonus);
                default:
                    return null;
            }
        }       

        #endregion
    }
}
