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

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> res = _context.Categories;
            if (res == null) return null;
            return res;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var res = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if (res == null) return null;
            return res;
        }

        public async Task<IEnumerable<Category>> SearchByCategoryName(string CategoryName)
        {
            IEnumerable<Category> res = await _context.Categories.Where(x => x.Name.ToLower() == CategoryName.ToLower()).ToListAsync();
            if (res == null) return null;
            return res;
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
