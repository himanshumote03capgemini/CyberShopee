using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Repository.Service
{
    public class CategoryRepository : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddCategory(Category category)
        {
            var res = _context.Categories.FirstOrDefault(x=>x.Name.ToLower() == category.Name.ToLower());
            if (res != null) return false;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var res = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if (res == null) return false;
            _context.Categories.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetAllCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            return categories.Select(c => new
            {
                c.CategoryId,
                c.Name,
                c.Description,
                Products = c.Products.Select(p => new
                {
                    p.ProductId,
                    p.CategoryId,
                    p.ModelNumber,
                    p.ModelName,
                    p.Cost,
                    p.Description,
                    p.ContentType,
                    ImageUrl = $"api/products/{p.ProductId}/image"
                })
            });
        }

        public async Task<object> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            if (category == null) return null;

            return new
            {
                category.CategoryId,
                category.Name,
                category.Description,
                Products = category.Products.Select(p => new
                {
                    p.ProductId,
                    p.CategoryId,
                    p.ModelNumber,
                    p.ModelName,
                    p.Cost,
                    p.Description,
                    p.ContentType,
                    Image = $"api/products/{p.ProductId}/image"
                })
            };
        }

        public async Task<IEnumerable<object>> SearchByCategoryName(string categoryName)
        {
            IEnumerable<Category> res = await _context.Categories.Where(x => x.Name.ToLower() == categoryName.ToLower()).ToListAsync();
            var categories = await _context.Categories
                .Where(c => c.Name.Contains(categoryName))
                .Include(c => c.Products)
                .ToListAsync();

            return categories.Select(c => new
            {
                c.CategoryId,
                c.Name,
                c.Description,
                Products = c.Products.Select(p => new
                {
                    p.ProductId,
                    p.CategoryId,
                    p.ModelNumber,
                    p.ModelName,
                    p.Cost,
                    p.Description,
                    p.ContentType,
                    ImageUrl = $"api/products/{p.ProductId}/image"
                }).ToList()
            });
        }

        public async Task<bool> UpdateCategory(int categoryId, Category category)
        {
            var res = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if (res == null) return false;

            res.Name = category.Name;
            res.Description = category.Description;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
