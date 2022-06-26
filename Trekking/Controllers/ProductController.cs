using System.Web.Mvc;

namespace Trekking.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            // ViewBag.result = ProductService.GetProducts();
            return View();
        }
        
        public ActionResult Show(string productId)
        {
            return View();
        }

    }
}