using System.ComponentModel.DataAnnotations;

namespace CyberShopee.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        [Required]
        public string Address { get; set; }


        // Navigation Property
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
