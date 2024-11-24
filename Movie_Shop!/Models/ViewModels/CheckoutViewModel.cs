namespace Movie_Shop_.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<ShoppingCartItem> CartItems { get; set; }

        public Customer ? CustomerDetails { get; set; }
    }
}
