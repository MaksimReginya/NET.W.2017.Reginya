using System.Web.Mvc;

namespace PL.ASP_NET.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // return RedirectToAction("ShowAccounts", "BankAccount");
            }

            return this.View();
        }
    }
}