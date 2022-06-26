using System.Web.Mvc;

namespace Trekking.Controllers
{
    public class UserController : Controller
    {
        // GET
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}