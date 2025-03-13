namespace CyberShopee.Models.DTO
{
    public class AuthResponseModel
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }        
        public string UserRole { get; set; }
        public string Token { get; set; }
    }
}
