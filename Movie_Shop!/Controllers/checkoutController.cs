using Microsoft.AspNetCore.Mvc;
using Movie_Shop_.Repositorey;

namespace Movie_Shop_.Controllers
{
    public class checkoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IShoppingCartService _shoppingCartService;
        public checkoutController(ICheckoutService checkoutService , IShoppingCartService shoppingCartService)
        {
            _checkoutService = checkoutService;
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
