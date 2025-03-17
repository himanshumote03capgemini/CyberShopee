using System.ComponentModel.DataAnnotations;

namespace CyberShopee.Models
{
    public class Admin
    {

        [Key] public int AdminId { get; set; }
        [Required] public string AdminName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }

        public string UserRole { get; set; } = "Admin";
    }
}
