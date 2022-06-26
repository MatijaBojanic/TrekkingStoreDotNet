using System.Web.Mvc;

namespace Trekking.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Show(string orderId)
        {
            return View();
        }
    }
}