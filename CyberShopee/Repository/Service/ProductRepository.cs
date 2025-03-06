using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;

namespace CyberShopee.Repository.Service
{
    public class ProductRepository : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(double minPrice, double maxPrice)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetTopSellingProducts(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> SearchProducts(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProduct(int ProductId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
