using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberShopee.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public DateTime ShipDate { get; set; }

        public string Status { get; set; } = "Pending";

        // Foreign Keys
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }


        // Navigation Properties
        public Customer Customer { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}
