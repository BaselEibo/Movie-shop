using System;
using Movie_Shop_.Db_Context;
using Movie_Shop_.Models;
using Movie_Shop_.Models.ViewModels;

namespace Movie_Shop_.Repositorey
{
    public class CheckoutService : ICheckoutService
    {
        private readonly Movie_DbContext _db;
        private readonly ILogger<CheckoutService> _logger;
        public CheckoutService(Movie_DbContext movie_DbContext , ILogger<CheckoutService> logger)
        {
            _db = movie_DbContext;
            _logger = logger;
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
            var theCustomer = GetOrCreateCustomer(customerDetails);

            var order = new Order
            {
                CustomerId = theCustomer.Id,
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice,
                orderRows = cart.Select(item => new OrderRow
                {
                    MovieId = item.MovieId,
                    Quantity= item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            try
            {
                _db.Orders.Add(order);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError ($"Error creating order for Customer ID: {theCustomer.Id}. Error: {ex.Message}");
                return false;
            }
           
        }

    }
}
