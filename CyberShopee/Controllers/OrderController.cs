using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _repository;
        public OrderController(IOrderRepo repository)
        {
            _repository = repository;
        }
    }
}
