using Estore.Areas.Admin.Models;
using Estore.Data;
using Estore.Helper;
using Estore.Models;
using Estore.Viewmodel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Estore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EstoreContext _context;

        public HomeController(ILogger<HomeController> logger, EstoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var allproducts = _context.Product.Take(10).ToList();

            var viewmodel = new HomeVM();

            viewmodel.Product = allproducts;

            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] int productId)
        {

            var product = _context.Product.FirstOrDefault(x=>x.ProductId == productId);
            if (product == null)
            {
                return Json(  new { success = false , msg = "Product not found" } );
            }

            //Take all data from Session if session is found, if session is not found then create a new CartItem for session
            var cart  = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            //get product from session where [FromBody] int productId is match 
            var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);


            if(cartItem == null)
            {
                cart.Add(new CartItem
                { 
                    ProductId =product.ProductId,
                    ProducName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }


            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return Json(new { success = true, msg = "Product added to cart" });

        }
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            int count = cart.Sum(x => x.Quantity);

            return Json(count);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
