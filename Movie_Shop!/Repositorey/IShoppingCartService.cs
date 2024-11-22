using Movie_Shop_.Models.ViewModels;

namespace Movie_Shop_.Repositorey
{
    public interface IShoppingCartService
    {
        List<ShoppingCartItem> GetCart();
        void AddToCart(ShoppingCartItem item);
        void RemoveFromCart(int movieId);
        void ChangeCartItemQuantity (int movieId, int quantity);
        void ClearCart();
        decimal GetCartTotal();
        void SaveCart(List<ShoppingCartItem> cart);
    }         
}
