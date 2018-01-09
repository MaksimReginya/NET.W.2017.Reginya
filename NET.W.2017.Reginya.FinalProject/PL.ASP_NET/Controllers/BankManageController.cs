using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.ServiceInterface;
using PL.ASP_NET.Utils.Mappers;
using PL.ASP_NET.ViewModels;

namespace PL.ASP_NET.Controllers
{
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
        
        #region Open account

        [HttpGet]
        public ActionResult OpenAccount() => throw new Exception();//this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception), View = "Error.html")]
        public ActionResult OpenAccount(ViewAccountModel account)
        {            
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            return this.View();
        }

        #endregion

        #region Show accounts

        [HttpGet]
        public ActionResult ShowAccounts()
        {
            return this.View(GetCurrentUserAccounts());
        } 

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "Error.html")]
        public ActionResult ShowAccounts(ViewAccountModel account)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private IEnumerable<ViewAccountModel> GetCurrentUserAccounts()
        {
            string userEmail = User.Identity.Name;
            var user = _bankManageService.GetUserInfo(userEmail);
            var userAccounts = user.Accounts.Select(account => account.ToViewModel());            
            return userAccounts;
        }

        #endregion
    }
}