using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Shop_.Models
{
    public class OrderRow
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int MovieId { get; set; }
        public Movie? movie { get; set; }
        public Order? order { get; set; }
    }
}
