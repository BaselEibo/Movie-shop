using System.ComponentModel.DataAnnotations;

namespace Movie_Shop_.Models.ViewModels
{
    public class ShoppingCartItem
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
    }
}
