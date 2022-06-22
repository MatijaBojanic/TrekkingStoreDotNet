using System.Web.Mvc;
using Trekking.Services;

namespace Trekking.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.result = ProductService.GetProducts();
            return View();
        }
        
        public ActionResult Show(string productId)
        {
            
            return View();
        }
        
        // GET: Product
        public ActionResult GetProduct(string productId)
        {
            return View();
        }
        
        // POST: Product
        public ActionResult PostProduct(string productId)
        {
            
            return View();
        }
    }
}