using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberShopee.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        // Foreign Keys
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        // Navigation Properties
        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
    }
}
