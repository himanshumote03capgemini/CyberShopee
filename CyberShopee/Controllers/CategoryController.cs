using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repository;
        public CategoryController(ICategoryRepo repository)
        {
            _repository = repository;
        }



    }
}
