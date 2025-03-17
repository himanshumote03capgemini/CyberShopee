using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CyberShopee.Models
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }


        // Navigation Property
        public ICollection<Product>? Products { get; set; }
    }
}
