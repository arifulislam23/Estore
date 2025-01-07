using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult ProductDetails()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
