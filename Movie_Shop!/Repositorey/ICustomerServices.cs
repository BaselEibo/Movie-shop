using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public interface ICustomerServices
    {
        Customer GetCustmerByEmail(string EmailAddress);
        
        public bool Create(Customer customer);
        
        public bool Update(Customer customer);

        public bool DeleteCustomer(int id);
    }
}
