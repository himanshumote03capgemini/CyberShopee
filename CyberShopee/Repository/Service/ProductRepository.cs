using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Repository.Service
{
    public class ProductRepository : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddProduct(IFormFile file, int categoryId, string modelNumber, string modelName, double cost, string description, int quantity)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return false; // No file uploaded

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var product = new Product
                    {
                        CategoryId = categoryId,
                        ModelNumber = modelNumber,
                        ModelName = modelName,
                        Cost = cost,
                        Description = description,
                        Quantity = quantity,
                        Image = memoryStream.ToArray(), // Store file as byte array
                        ContentType = file.ContentType  // Store image type
                    };

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false; // Handle errors gracefully
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var res = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (res == null) return false;
            _context.Products.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var res = _context.Products;
            if(res==null) return Enumerable.Empty<Product>();
            return res;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var res = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (res == null) return null;
            return res;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var res = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (res == null) return Enumerable.Empty<Product>();
            return res;

        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(double minPrice, double maxPrice)
        {
            var res = await _context.Products.Where(x => x.Cost >=minPrice && x.Cost <= maxPrice).ToListAsync();
            if (res == null) return Enumerable.Empty<Product>();
            return res;
        }

        public async Task<IEnumerable<Product>> GetTopSellingProducts(int count)
        {
            var res = await _context.Products
                                .OrderByDescending(p => p.OrderDetails.Sum(od => od.Quantity))
                                .Take(count)
                                .ToListAsync();
            if (res == null) return null;
            return res;

        }

        public async Task<IEnumerable<Product>> SearchProducts(string name)
        {
            var res = await _context.Products.Where(x => x.ModelName.ToLower().Contains(name.ToLower())).ToListAsync();
            if (res == null) return null;
            return res;
        }

        public async Task<bool> UpdateProduct(int productId, IFormFile file, int categoryId, string modelNumber, string modelName, double cost, string description, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return false;

            // Update product details
            product.CategoryId = categoryId;
            product.ModelNumber = modelNumber;
            product.ModelName = modelName;
            product.Cost = cost;
            product.Description = description;
            product.Quantity = quantity;

            // Update image only if a new file is uploaded
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    product.Image = memoryStream.ToArray();
                    product.ContentType = file.ContentType;
                }
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
