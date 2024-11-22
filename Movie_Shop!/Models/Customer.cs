using System.ComponentModel.DataAnnotations;

namespace Movie_Shop_.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string BillingZip { get; set; } = string.Empty;
        public string BillingCity { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string DeliverZip { get; set; } = string.Empty;
        public string DeliverCity { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
