using Microsoft.AspNetCore.Http;
using Movie_Shop_.Models.ViewModels;
using System.Text.Json;

namespace Movie_Shop_.Repositorey
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ShoppingCartService> _logger;
        private const string SessionsKey = "ShoppingCart";

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, ILogger<ShoppingCartService> logger)
        {
            _contextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger;
        }

        public List<ShoppingCartItem>? GetCart()
        {
            var cartJson = _contextAccessor.HttpContext?.Session?.GetString (SessionsKey);
            return string.IsNullOrEmpty(cartJson)
                ?  new List<ShoppingCartItem>()
                :  JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJson); 
   
        }


        public void SaveCart(List<ShoppingCartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            _contextAccessor.HttpContext?.Session?.SetString (SessionsKey, cartJson);
        }


        public void AddToCart(ShoppingCartItem item)
        {
            var cart = GetCart();
            var existingItem =  cart.FirstOrDefault(c => c.MovieId == item.MovieId);
            if (existingItem != null)
            {
                existingItem.Quantity ++;
            }
            else 
            {
                cart.Add(item); 
            }
            
            SaveCart(cart);
            
        }


        public void RemoveFromCart(int movieId)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(c => c.MovieId == movieId);

            if (existingItem != null)
            {
                if (existingItem.Quantity > 1)
                {
                    existingItem.Quantity--;
                }
                else
                {
                    cart.Remove(existingItem);
                }
                
            }

           SaveCart (cart);
        }


        public void ChangeCartItemQuantity(int movieId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.MovieId == movieId );
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }
            SaveCart(cart);
        }

        


        public decimal GetCartTotal()
        {
            var cart = GetCart();
           return cart.Sum(c => c.Quantity * c.Price);
            
        }



        public void ClearCart()
        {
           _contextAccessor.HttpContext.Session.Remove(SessionsKey);
        }

    }
}
