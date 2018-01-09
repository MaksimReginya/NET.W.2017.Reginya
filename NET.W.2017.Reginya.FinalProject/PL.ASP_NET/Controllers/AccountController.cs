using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.ServiceInterface;
using PL.ASP_NET.ViewModels;

namespace PL.ASP_NET.Controllers
{
    public class AccountController : Controller
    {
        #region Private fields
                
        private readonly IBankManageService _bankManageService;

        #endregion

        #region Public constructors

        public AccountController(IBankManageService bankManageService)
        {
            this._bankManageService = bankManageService;
        }

        #endregion

        #region Registration

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("ShowAccounts", "BankManage");
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error.html")]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (!(_bankManageService.GetUserInfo(viewModel.Email) is null))
            {
                ModelState.AddModelError(string.Empty, "User with this email already registered.");
                return this.View(viewModel);
            }

            _bankManageService.RegisterUser(viewModel.Email, viewModel.Password, viewModel.FirstName, viewModel.LastName);
            FormsAuthentication.SetAuthCookie(viewModel.Email, false);
            return this.RedirectToAction("Index", "Home");
        }

        #endregion

        #region Log in
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankManageServiceException), View = "Error.html")]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Incorrect email or password.");
                ViewBag.LoginClick = true;
                return this.View(viewModel);
            }

            try
            {
                _bankManageService.VerifyRegistration(viewModel.Email, viewModel.Password);
                FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction("Index", "Home");
            }
            catch (BankManageServiceException)
            {
                ModelState.AddModelError(string.Empty, "Incorrect email or password.");
                ViewBag.LoginClick = true;
                return this.View(viewModel);
            }
        }

        #endregion

        #region Log out

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        #endregion 
    }
}