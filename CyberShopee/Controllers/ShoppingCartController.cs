using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepo _repository;
        public ShoppingCartController(IShoppingCartRepo repository)
        {
            _repository = repository;
        }
    }
}
