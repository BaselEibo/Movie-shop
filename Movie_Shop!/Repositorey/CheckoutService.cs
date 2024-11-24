using System;
using Movie_Shop_.Db_Context;
using Movie_Shop_.Models;
using Movie_Shop_.Models.ViewModels;

namespace Movie_Shop_.Repositorey
{
    public class CheckoutService : ICheckoutService
    {
        private readonly Movie_DbContext _db;
        private readonly IShoppingCartService _shopCartService;
        private readonly ILogger<CheckoutService> _logger;
        public CheckoutService(Movie_DbContext movie_DbContext , ILogger<CheckoutService> logger , IShoppingCartService shoppingCartService)
        {
            _db = movie_DbContext;
            _logger = logger;
            _shopCartService = shoppingCartService;
        }


        public bool ValidateCart(List<ShoppingCartItem> Cart)
        {
          if (Cart == null || !Cart.Any())
            {
                return false;
            }
          else if (Cart.Any(item => item.Quantity <= 0 || item.Price <= 0))
            {
                return false;
            }
          return true;
        }


        public Customer GetOrCreateCustomer(Customer customerDetails)
        {
            var existingCustomer = _db.Customers
            .FirstOrDefault(c => c.EmailAddress == customerDetails.EmailAddress);
            if (existingCustomer != null)
            {
                return existingCustomer;
            }
            
                _db.Customers.Add(customerDetails);
                _db.SaveChanges();
                return customerDetails;

        }


        public bool ProcessOrder(List<ShoppingCartItem> cart, Customer customerDetails)
        {
            if (!ValidateCart(cart))
            {
                return false;
            }

            var Customer = GetOrCreateCustomer(customerDetails);
            var totalPrice = _shopCartService.GetCartTotal();

            var order = new Order
            {
                CustomerId = Customer.Id,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                orderRows = cart.Select(item => new OrderRow
                {
                    MovieId = item.MovieId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()

            };
            _db.Orders.Add(order);
            _db.SaveChanges();

                return true;
        
        }

    }
}
