using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public interface ICustomerServices
    {
        List<Order> GetOrdersByCustomer(int CustomerId);
        List<Customer> GetAllCustomer();
        Customer GetCustmerByEmail(string EmailAddress);
        
        bool Create(Customer customer);
        
        bool Update(Customer customer);

        bool DeleteCustomer(int id);
    }
}
