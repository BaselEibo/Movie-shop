using Movie_Shop_.Models;
using Movie_Shop_.Models.ViewModels;

namespace Movie_Shop_.Repositorey
{
    public interface ICheckoutService
    {
        bool ValidateCart(List<ShoppingCartItem> Cart);
        Customer GetOrCreateCustomer(Customer customerDetails);
        bool ProcessOrder(List<ShoppingCartItem> cart, Customer customerDetails);

    }
}
