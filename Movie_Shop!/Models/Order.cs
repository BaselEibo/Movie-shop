using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Shop_.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Id is required.")]
        [ForeignKey("Customer")]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public Customer? Customers { get; set; }

        public List<OrderRow> orderRows { get; set; } = new List<OrderRow>();
    }
}
