using CyberShopee.Models;
using CyberShopee.Models.DTO;

namespace CyberShopee.Repository.DAO
{
    public interface IAdminRepo
    {
        AuthResponseModel? Login(LoginModel login);
    }
}
