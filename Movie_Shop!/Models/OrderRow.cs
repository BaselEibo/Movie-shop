using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Shop_.Models
{
    public class OrderRow
    {
        public int Id { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Order Id is required.")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Movie Id is required.")]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie? movie { get; set; }
        public Order? order { get; set; }
    }
}
