using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using ASP_NET_PL.ViewModels;
using BLL.Interface.ServiceInterface;
using BLL.Interface.MailServiceInterface;

namespace ASP_NET_PL.Controllers
{
    public class AccountController : Controller
    {
        #region Private fields

        private readonly string _hostEmailAddress = ConfigurationManager.AppSettings["ServiceEmailAddress"];
        private readonly string _hostEmailPassword = ConfigurationManager.AppSettings["ServiceEmailPassword"];
        private readonly IBankAccountService _bankAccountService;
        private readonly IAccountNumberGenerator _numberGenerator;
        private readonly IMailService _mailService;

        #endregion

        #region Public constructors
                
        public AccountController(IBankAccountService bankAccountService, IMailService mailService, IAccountNumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
            _bankAccountService = bankAccountService;
            _mailService = mailService;
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
                account.OwnerSecondName, account.OwnerEmail, account.Balance, account.Bonus);

            string subject = "Account is successfully opened!";
            string message = $"Your account number: {openedAccountNumber}";
            await SendMailAsync(account.OwnerEmail, subject, message);

            TempData["accountOpened"] = true;
            return RedirectToAction("AccountIsOpened");
        }

        [HttpGet]
        public ActionResult AccountIsOpened()
        {
            var accountOpened = TempData["accountOpened"] as bool?;
            if (!accountOpened.HasValue || !accountOpened.Value)
            {
                return RedirectToAction("OpenAccount");
            }

            TempData["accountOpened"] = false;
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
                From = _hostEmailAddress,
                Password = _hostEmailPassword,
                Subject = subject,
                Message = message
            };
            return _mailService.SendMailAsync(mailData);
        }
        
        #endregion
    }
}