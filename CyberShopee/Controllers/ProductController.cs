using CyberShopee.Repository.DAO;
using CyberShopee.Repository.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repository;
        public ProductController(IProductRepo repository)
        {
            _repository = repository;
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct(
            IFormFile file,
            [FromForm] int categoryId,
            [FromForm] string modelNumber,
            [FromForm] string modelName,
            [FromForm] double cost,
            [FromForm] string description,
            [FromForm] int quantity)
        {
            var result = await _repository.AddProduct(file, categoryId, modelNumber, modelName, cost, description, quantity);
            if (!result)
                return BadRequest("Failed to add product. Please check input data.");

            return Ok("Product added successfully.");
        }

        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(
            int productId,
            IFormFile file,
            [FromForm] int categoryId,
            [FromForm] string modelNumber,
            [FromForm] string modelName,
            [FromForm] double cost,
            [FromForm] string description,
            [FromForm] int quantity)
        {
            var result = await _repository.UpdateProduct(productId, file, categoryId, modelNumber, modelName, cost, description, quantity);
            if (!result)
                return NotFound("Product not found or update failed.");

            return Ok("Product updated successfully.");
        }
    }
}
