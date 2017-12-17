using System;
using System.Configuration;
using System.Web.Mvc;
using BLL.Interface.ServiceInterface;
using BLL.Interface.MailServiceInterface;
using ASP_NET_PL.Models.ViewModels;
using System.Threading.Tasks;

namespace ASP_NET_PL.Controllers
{
    public class AccountController : Controller
    {
        #region Private fields
                
        private readonly IBankAccountService _bankAccountService;
        private readonly IAccountNumberGenerator _numberGenerator;
        private readonly IMailService _mailService;

        #endregion

        #region Public constructors
                
        public AccountController()
        {
            _numberGenerator = DependencyFactory.NumberGenerator;
            _bankAccountService = DependencyFactory.BankAccountService;
            _mailService = DependencyFactory.MailService;
        }

        #endregion

        #region Actions

        #region Open account
        
        [HttpGet]
        public ActionResult OpenAccount() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public async Task<ActionResult> OpenAccount(ViewAccountModel account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string openedAccountNumber = _bankAccountService.CreateAccount(account.Type, _numberGenerator, account.OwnerFirstName,
                account.OwnerSecondName, account.OwnerEmail, account.Sum, account.Bonus);

            string subject = "Account is successfully opened!";
            string message = $"Your account ID: {openedAccountNumber}";
            await this.SendMailAsync(account.OwnerEmail, subject, message);

            Session["accountOpened"] = true;
            return RedirectToAction("AccountIsOpened");
        }

        [HttpGet]
        public ActionResult AccountIsOpened()
        {
            var accountOpened = Session["accountOpened"] as bool?;
            if (!accountOpened.HasValue || !accountOpened.Value)
            {
                return RedirectToAction("OpenAccount");
            }

            Session["accountOpened"] = false;
            return View();
        }

        #endregion
        
        #endregion

        #region Private methods
        
        private Task SendMailAsync(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = ConfigurationManager.AppSettings["ServiceEmailAddress"],
                Password = ConfigurationManager.AppSettings["ServiceEmailPassword"],
                Subject = subject,
                Message = message
            };
            return _mailService.SendMailAsync(mailData);
        }
        
        #endregion
    }
}