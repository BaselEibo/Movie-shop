using Movie_Shop_.Db_Context;
using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public class CustomerServices : ICustomerServices
    {
        private readonly Movie_DbContext _db;
        private readonly ILogger<CustomerServices> _logger; 
        public CustomerServices(Movie_DbContext movie_DbContext , ILogger<CustomerServices>  logger )
        {
            _logger = logger;
            _db = movie_DbContext;
        }
        public Customer GetCustmerByEmail(string emailAddress)
        {
            return _db.Customers.FirstOrDefault(c => c.EmailAddress == emailAddress);   
        }
        public bool Create(Customer customer)
        {
            try
            {
                if (_db.Customers.Any(c => c.EmailAddress == customer.EmailAddress))
                {
                    return false;
                }
                _db.Customers.Add(customer);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating customer. Title: {customer?.EmailAddress}, Error: {ex.Message}");
                return false;
            }
        }


        public bool Update(Customer customer)
        {
            try
            {
            var existingCustomer = _db.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existingCustomer == null)
                {
                    return false ;
                }
                existingCustomer.EmailAddress = customer.EmailAddress;
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.PhoneNo = customer.PhoneNo;
                existingCustomer.BillingZip= customer.BillingZip;
                existingCustomer.BillingAddress = customer.BillingAddress;
                existingCustomer.BillingCity = customer.BillingCity;
                existingCustomer.DeliverCity = customer.DeliverCity;
                existingCustomer.DeliverZip = customer.DeliverZip;
                existingCustomer.DeliveryAddress = customer.DeliveryAddress;

                
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating customer with Id: {customer.Id}. Error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return false;
            }



        }

        public bool DeleteCustomer(int id)
        {
           var customer = _db.Customers.FirstOrDefault(c => c.Id==id);
            if (customer == null)
            {
                return false;
            }
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return true;
        }

        public List<Order> GetOrdersByCustomer(int CustomerId)
        {
           var orders = _db.Orders.Where(o => o.CustomerId==CustomerId).ToList();
            return orders;
        }

        public List<Customer> GetAllCustomer()
        {
            return _db.Customers.ToList();
        }
    }
}
