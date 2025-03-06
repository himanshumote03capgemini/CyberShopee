using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;

namespace CyberShopee.Repository.Service
{
    public class ShoppingCartRepository : IShoppingCartRepo
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddToCart(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFromCart(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCart()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShoppingCart>> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetById(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCart(int shoppingCartId, ShoppingCart cart)
        {
            throw new NotImplementedException();
        }
    }
}
