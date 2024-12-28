using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public interface ICustomerServices
    {
        public List<Order> GetOrdersByCustomer(int CustomerId);
        public List<Customer> GetAllCustomer();
        Customer GetCustmerByEmail(string EmailAddress);
        
        public bool Create(Customer customer);
        
        public bool Update(Customer customer);

        public bool DeleteCustomer(int id);
    }
}
