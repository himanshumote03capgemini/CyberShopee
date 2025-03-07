using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberShopee.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ModelNumber { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public double Cost { get; set; }        // cost per unit
        public string Description { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public byte[] Image { get; set; } // Storing image as byte array
        public string ContentType { get; set; } // Image type

        // Foreign Keys
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
        //public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
