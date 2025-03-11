namespace CyberShopee.Models.DTO
{
    public class AuthResponseModel
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }        // email
        public string UserRole { get; set; }
        public string Token { get; set; }
    }
}
