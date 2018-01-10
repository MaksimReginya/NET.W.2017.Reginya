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
        [HandleError(ExceptionType = typeof(Exception), View = "Error.html")]
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

        #region Close account

        [HttpGet]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error.html")]
        public ActionResult CloseAccount(string accountNumber)
        {
            ViewBag.ClosingAccountNumber = accountNumber;
            return this.View();
        } 
                    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error.html")]
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