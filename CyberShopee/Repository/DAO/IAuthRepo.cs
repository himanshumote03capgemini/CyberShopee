using CyberShopee.Models.DTO;

namespace CyberShopee.Repository.DAO
{
    public interface IAuthRepo
    {
        AuthResponseModel? Login(LoginModel login);
    }
}
