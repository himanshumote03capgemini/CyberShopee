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

        // Get all products
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllProducts()
        {
            var products = await _repository.GetAllProducts();
            var productList = products.Select(p => new
            {
                p.ProductId,
                p.CategoryId,
                p.ModelNumber,
                p.ModelName,
                p.Cost,
                p.Description,
                p.ContentType,
                ImageUrl = $"api/products/{p.ProductId}/image"
            });

            return Ok(productList);
        }

        // Get product by ID
        [HttpGet("{productId}")]
        public async Task<ActionResult<object>> GetProductById(int productId)
        {
            var product = await _repository.GetProductById(productId);
            if (product == null) return NotFound("Product not found.");

            var productData = new
            {
                product.ProductId,
                product.CategoryId,
                product.ModelNumber,
                product.ModelName,
                product.Cost,
                product.Description,
                product.ContentType,
                ImageUrl = $"api/products/{product.ProductId}/image"
            };

            return Ok(productData);
        }

        // Get products by category ID
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _repository.GetProductsByCategoryId(categoryId);
            var productList = products.Select(p => new
            {
                p.ProductId,
                p.CategoryId,
                p.ModelNumber,
                p.ModelName,
                p.Cost,
                p.Description,
                p.ContentType,
                ImageUrl = $"api/products/{p.ProductId}/image"
            });

            return Ok(productList);
        }

        // Search products by name
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<object>>> SearchProducts(string name)
        {
            var products = await _repository.SearchProducts(name);
            var productList = products.Select(p => new
            {
                p.ProductId,
                p.CategoryId,
                p.ModelNumber,
                p.ModelName,
                p.Cost,
                p.Description,
                p.ContentType,
                ImageUrl = $"api/products/{p.ProductId}/image"
            });

            return Ok(productList);
        }

        // Get products by price range
        [HttpGet("price-range")]
        public async Task<IActionResult> GetProductsByPriceRange([FromQuery] double minPrice, [FromQuery] double maxPrice)
        {
            var products = await _repository.GetProductsByPriceRange(minPrice, maxPrice);
            var productList = products.Select(p => new
            {
                p.ProductId,
                p.CategoryId,
                p.ModelNumber,
                p.ModelName,
                p.Cost,
                p.Description,
                p.ContentType,
                ImageUrl = $"api/products/{p.ProductId}/image"
            });
            return Ok(productList);
        }

        // Get top-selling products
        [HttpGet("top-selling/{count}")]
        public async Task<IActionResult> GetTopSellingProducts(int count)
        {
            var products = await _repository.GetTopSellingProducts(count);
            var productList = products.Select(p => new
            {
                p.ProductId,
                p.CategoryId,
                p.ModelNumber,
                p.ModelName,
                p.Cost,
                p.Description,
                p.ContentType,
                ImageUrl = $"api/products/{p.ProductId}/image"
            });
            return Ok(productList);
        }

        // Add a new product
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

        // Update an existing product
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
