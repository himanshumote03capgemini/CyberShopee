using CyberShopee.Models;
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

        // Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _repository.GetAllCategories();
            if (categories == null) return NoContent();
            return Ok(categories);
        }

        // Get category by ID
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _repository.GetCategoryById(categoryId);
            if (category == null) return NotFound("Category not found.");
            return Ok(category);
        }

        // Add a new category
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.AddCategory(category);
                if (!result) return StatusCode(500, "Failed to add category.");

                return Ok("Category added successfully.");
            }
            return BadRequest("Invalid category data.");
        }

        // Update category
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateCategory(categoryId, category);
                if (!result) return NotFound("Category not found or update failed.");

                return Ok("Category updated successfully.");
            }
            return BadRequest("Invalid category data.");
        }

        // Delete category
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var result = await _repository.DeleteCategory(categoryId);
            if (!result) return NotFound("Category not found or delete failed.");

            return Ok("Category deleted successfully.");
        }

        // Search category by name
        [HttpGet("search/{categoryName}")]
        public async Task<IActionResult> SearchByCategoryName(string categoryName)
        {
            var categories = await _repository.SearchByCategoryName(categoryName);
            if (categories == null) return NotFound("Category not found.");
            return Ok(categories);
        }

    }
}
