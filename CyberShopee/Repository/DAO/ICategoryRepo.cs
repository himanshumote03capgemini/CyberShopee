using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetAllCategories();

        Task<Category> GetCategoryById(int categoryId);

        Task<bool> AddCategory(Category category);

        Task<bool> UpdateCategory(int categoryId, Category category);

        Task<bool> DeleteCategory(int categoryId);

        Task<IEnumerable<Category>> SearchByCategoryName(string CategoryName);
    }
}
