using CyberShopee.CustomException;
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
                    throw new ProductException("Image is Required");

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
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            return products;

        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(double minPrice, double maxPrice)
        {
            var products = await _context.Products.Where(x => x.Cost >=minPrice && x.Cost <= maxPrice).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetTopSellingProducts(int count)
        {
            var products = await _context.Products
                                .OrderByDescending(p => p.OrderDetails.Sum(od => od.Quantity))
                                .Take(count)
                                .ToListAsync();
            return products;

        }

        public async Task<IEnumerable<Product>> SearchProducts(string name)
        {
            var products = await _context.Products.Where(x => x.ModelName.ToLower().Contains(name.ToLower()) || x.ModelNumber.ToLower().Contains(name.ToLower())).ToListAsync();
            return products;
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
