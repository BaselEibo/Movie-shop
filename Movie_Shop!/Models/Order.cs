using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Shop_.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer? Customers { get; set; }
        public List<OrderRow> orderRows { get; set; } = new List<OrderRow>();
    }
}
