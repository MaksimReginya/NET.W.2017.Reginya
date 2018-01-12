using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.ServiceInterface;
using PL.ASP_NET.Utils.Mappers;
using PL.ASP_NET.ViewModels;

namespace PL.ASP_NET.Controllers
{
    [Authorize]
    public class BankManageController : Controller
    {
        #region Private fields

        private readonly IBankManageService _bankManageService;

        #endregion

        #region Public constructors

        public BankManageController(IBankManageService bankManageService)
        {
            _bankManageService = bankManageService;
        }

        #endregion

        #region Public actions
                    
        #region Open account

        [HttpGet]        
        public ActionResult OpenAccount() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult OpenAccount(OpenAccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string userEmail = User.Identity.Name;
            var accountNumber = _bankManageService.CreateAccount(userEmail, viewModel.Password, viewModel.Type, viewModel.Balance);            
            if (_bankManageService.GetAccountInfo(userEmail, accountNumber) is null)
            {
                return RedirectToAction("OpenAccount");
            }
            
            return View("AccountIsOpened");
        }

        #endregion

        #region Show accounts

        [HttpGet]
        public ActionResult ShowAccounts() => this.View(GetCurrentUserAccounts());

        #endregion

        #region Deposit

        [HttpGet]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult Deposit(string accountNumber)
        {
            ViewBag.DepositingAccountNumber = accountNumber;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult Deposit(DepositWithdrawViewModel depositData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string userEmail = User.Identity.Name;
            _bankManageService.Deposit(userEmail, depositData.AccountNumber, depositData.OperationSum);
            return RedirectToAction("ShowAccounts");
        }

        #endregion

        #region Withdraw

        [HttpGet]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult Withdraw(string accountNumber, int? balance)
        {
            ViewBag.WithdrawingAccountNumber = accountNumber;
            ViewBag.Balance = balance;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult Withdraw(DepositWithdrawViewModel withdrawData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string userEmail = User.Identity.Name;
            _bankManageService.Withdraw(userEmail, withdrawData.AccountNumber, withdrawData.OperationSum);
            return RedirectToAction("ShowAccounts");
        }

        #endregion

        #region Close account

        [HttpGet]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult CloseAccount(string accountNumber)
        {
            ViewBag.ClosingAccountNumber = accountNumber;
            return this.View();
        } 
                    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error")]
        public ActionResult CloseAccount(CloseAccountViewModel closeAccountData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            string userEmail = User.Identity.Name;
            _bankManageService.CloseAccount(userEmail, closeAccountData.Password, closeAccountData.AccountNumber);            
            return RedirectToAction("ShowAccounts");
        }

        #endregion

        #endregion

        #region Private methods

        private IEnumerable<AccountViewModel> GetCurrentUserAccounts()
        {
            string userEmail = User.Identity.Name;
            var user = _bankManageService.GetUserInfo(userEmail);
            var userAccounts = user.Accounts.Select(account => account.ToViewModel());            
            return userAccounts;
        }

        #endregion
    }
}