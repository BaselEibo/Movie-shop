using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Movie_Shop_.Models;
using Movie_Shop_.Models.ViewModels;
using Movie_Shop_.Repositorey;

namespace Movie_Shop_.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;
        private readonly IMovieServices _MS;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(IShoppingCartService cartService, ILogger<ShoppingCartController> logger, IMovieServices movieServices )
        {
            _cartService = cartService;
            _logger = logger;
            _MS = movieServices;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int movieId) 
        {
            var movie = _MS.GetMovieById(movieId);

            if (movie == null)
            {
                TempData["Error"] = "Movie not found.";
                return RedirectToAction("MovieList", nameof(MovieController));
            }

            var cartItem = new ShoppingCartItem
            {
                MovieId = movie.Id,
                Title = movie.Title,
                Price = movie.Price,
                Quantity = 1
            };

            _cartService.AddToCart(cartItem);
            TempData["Message"] = $"Added '{movie.Title}' to your cart.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int movieId)
        {
            _cartService.RemoveFromCart(movieId);
            TempData["Message"] = "Item removed from the cart.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ChangeQuantity(int movieId, int quantity)
        {
            _cartService.ChangeCartItemQuantity(movieId, quantity);
            TempData["Message"] = "Cart updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
