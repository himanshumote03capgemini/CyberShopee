using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductById(int productId);

        Task<bool> AddProduct(Product product);

        Task<bool> UpdateProduct(int ProductId, Product product);

        Task<bool> DeleteProduct(int productId);

        Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);

        Task<IEnumerable<Product>> SearchProducts(string keyword);

        Task<IEnumerable<Product>> GetProductsByPriceRange(double minPrice, double maxPrice);

        Task<IEnumerable<Product>> GetTopSellingProducts(int count);
    }
}
