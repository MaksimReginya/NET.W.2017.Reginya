using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Helpers;
using BLL.Interface.Entities;
using BLL.Interface.MailServiceInterface;
using BLL.Interface.ServiceInterface;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.DTO;

namespace BLL.ServiceImplementation
{
    public class BankManageService : IBankManageService
    {
        #region Private fields

        private readonly IMailService _mailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountOwnerRepository _ownerRepository;
        private readonly IAccountService _accountService;

        private readonly string _hostEmail;
        private readonly string _hostEmailPassword;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankManageService"/> class.
        /// </summary>
        /// <param name="mailService">Mail service.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        /// <param name="ownerRepository">Account owner repository.</param>
        /// <param name="accountService">Account service.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one of the arguments is null.
        /// </exception>
        public BankManageService(
            IMailService mailService,
            IUnitOfWork unitOfWork,
            IAccountOwnerRepository ownerRepository,
            IAccountService accountService)
        {
            VerifyIsNull(mailService, nameof(mailService));
            VerifyIsNull(unitOfWork, nameof(unitOfWork));
            VerifyIsNull(ownerRepository, nameof(ownerRepository));
            VerifyIsNull(accountService, nameof(accountService));

            _mailService = mailService;
            _unitOfWork = unitOfWork;
            _ownerRepository = ownerRepository;
            _accountService = accountService;

            _hostEmail = WebConfigurationManager.AppSettings["AppEmail"];
            _hostEmailPassword = WebConfigurationManager.AppSettings["AppPassword"];
        }

        #endregion

        #region Implementation of IBankManageService
      
        /// <inheritdoc />
        public void RegisterUser(string email, string password, string userFirstName, string userSecondName)
        {
            var newOwner = new AccountOwner(userFirstName, userSecondName, email);
            VerifyIsValidString(password, nameof(password));
            try
            {
                var owner = _ownerRepository.GetOwnerByEmail(newOwner.Email);
                if (!(owner is null))
                {
                    throw new BankManageServiceException($"User with email {newOwner.Email} is already registered.");
                }

                string passwordHash = Crypto.HashPassword(password);
                _ownerRepository.AddOwner(newOwner.ToDtoAccountOwner(passwordHash));             
                this.SendMail(newOwner.Email, "Bank account service", "Congratulations! You have successfully registered in our service.");
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {                
                throw new BankManageServiceException("Some error during registration occurred.", ex);
            }
        }

        /// <inheritdoc />
        public AccountOwner GetUserInfo(string email)
        {
            try
            {
                var owner = _ownerRepository.GetOwnerByEmail(email);
                return owner?.ToBllAccountOwner();
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during getting information about owner occurred.", ex);
            }
        }

        /// <inheritdoc />
        public void VerifyRegistration(string email, string password)
        {
            VerifyEmail(email, nameof(email));
            VerifyIsValidString(password, nameof(password));

            DtoAccountOwner owner;
            try
            {
                owner = _ownerRepository.GetOwnerByEmail(email);
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during verifying registration of user.", ex);
            }

            if (owner is null)
            {
                throw new BankManageServiceException($"User with specified email: \"{email}\" doesn't exist.");
            }
            
            if (!Crypto.VerifyHashedPassword(owner.Password, password))
            {
                throw new BankManageServiceException($"User with specified email: \"{email}\", and password: \"{password}\" doesn't exist.");
            }
        }
                    
        /// <inheritdoc />
        public string CreateAccount(
            string email,
            string password,
            AccountType type,
            decimal balance = 0m,
            int bonus = 0)
        {           
            this.VerifyRegistration(email, password);
            try
            {
                var owner = _ownerRepository.GetOwnerByEmail(email).ToBllAccountOwner();
                string accountNumber = _accountService.CreateAccount(owner, type, balance, bonus);                               
                this.SendMail(
                    owner.Email,
                    "Bank account service",
                    $"Congratulations! You have successfully created account in our service. Unique number of account: {accountNumber}");

                _unitOfWork.Commit();
                return accountNumber;
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during account creation occurred.", ex);
            }
        }

        /// <inheritdoc />
        public void Deposit(string email, string accountNumber, decimal value)
        {
            VerifyIsValidString(accountNumber, nameof(accountNumber));
            if (GetUserInfo(email) is null)
            {
                throw new BankManageServiceException($"User with specified email: \"{email}\" doesn't exist.");
            } 

            try
            {
                _accountService.Deposit(email, accountNumber, value);
                this.SendMail(
                    email,
                    "Bank account service",
                    $"Deposit operation completed successfully. Account: {accountNumber}, Deposit sum: {value}.");

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during deposit creation occurred.", ex);
            }
        }

        /// <inheritdoc />
        public void Withdraw(string email, string accountNumber, decimal value)
        {
            VerifyIsValidString(accountNumber, nameof(accountNumber));
            if (GetUserInfo(email) is null)
            {
                throw new BankManageServiceException($"User with specified email: \"{email}\" doesn't exist.");
            }

            try
            {
                _accountService.Withdraw(email, accountNumber, value);
                this.SendMail(
                    email,
                    "Bank account service",
                    $"Withdraw operation completed successfully. Account: {accountNumber}, Withdraw sum: {value}.");

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during withdraw creation occurred.", ex);
            }
        }

        /// <inheritdoc />
        public void MoneyTransfer(string fromEmail, string fromAccountNumber, string toEmail, string toAccountNumber, decimal value)
        {
            VerifyIsValidString(fromAccountNumber, nameof(fromAccountNumber));
            VerifyIsValidString(toAccountNumber, nameof(toAccountNumber));
            if (GetUserInfo(fromEmail) is null)
            {
                throw new BankManageServiceException($"User with specified email: \"{fromAccountNumber}\" doesn't exist.");
            }

            if (GetUserInfo(toEmail) is null)
            {
                throw new BankManageServiceException($"User with specified email: \"{toEmail}\" doesn't exist.");
            }

            try
            {
                _accountService.Withdraw(fromEmail, fromAccountNumber, value);
                _accountService.Deposit(toEmail, toAccountNumber, value);

                string message =
                    $"Money successfully transfered from your account. Account: {fromAccountNumber}, Withdraw sum: {value}. " +
                    $"Account: {toAccountNumber}, Deposit sum: {value}";
                this.SendMail(
                    fromEmail,
                    "Bank account service",
                    message);

                this.SendMail(
                    toEmail,
                    "Bank account service",
                    $"Money successfully transfered to your account. Account: {toAccountNumber}, Deposit sum: {value}");

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during money transfer occurred.", ex);
            }
        }

        /// <inheritdoc />
        public string GetAccountInfo(string email, string accountNumber)
        {
            VerifyIsValidString(accountNumber, nameof(accountNumber));            
            try
            {
                return _accountService.GetAccountInfo(email, accountNumber);
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during getting information about account occurred.", ex);
            }
        }

        /// <inheritdoc />
        public void CloseAccount(string email, string password, string accountNumber)
        {
            VerifyIsValidString(accountNumber, nameof(accountNumber));
            this.VerifyRegistration(email, password);
            try
            {
                _accountService.CloseAccount(email, accountNumber);
                this.SendMail(
                    email,
                    "Bank account service",
                    $"Account with number: {accountNumber} has been successfully closed.");

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new BankManageServiceException("Some error during closing account occurred.", ex);
            }
        }
        
        #endregion 
        
        #region Private methods

        private static void VerifyIsNull(object obj, string paramName)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} can't be null.");
            }
        }

        private static void VerifyIsValidString(string str, string paramName)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException($"{paramName} can't be null or white space.", paramName);
            }
        }

        private static void VerifyEmail(string email, string paramName)
        {
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{email} is invalid email.", paramName);
            }
        }
        
        private void SendMail(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = _hostEmail,
                Password = _hostEmailPassword,
                Subject = subject,
                Message = message
            };

            _mailService.SendMail(mailData);
        }
                
        #endregion
    }
}
