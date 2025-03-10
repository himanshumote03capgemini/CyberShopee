using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface ICategoryRepo
    {

        Task<bool> AddCategory(Category category);

        Task<bool> UpdateCategory(int categoryId, Category category);

        Task<bool> DeleteCategory(int categoryId);

        Task<IEnumerable<object>> GetAllCategories();
        
        Task<object> GetCategoryById(int categoryId);
        
        Task<IEnumerable<object>> SearchByCategoryName(string categoryName);

    }
}
