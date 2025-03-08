using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Repository.Service
{
    public class ShoppingCartRepository : IShoppingCartRepo
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddToCart(ShoppingCart cart)
        {
            await _context.ShoppingCarts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFromCart(int shoppingCartId)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(shoppingCartId);
            if (cartItem == null) return false;

            _context.ShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCart()
        {
            var res = await _context.ShoppingCarts.ToListAsync();
            return res;
        }

        public async Task<IEnumerable<ShoppingCart>> GetByCustomerId(int customerId)
        {
            var res = await _context.ShoppingCarts
                        .Where(c => c.CustomerId == customerId)
                        .ToListAsync();
            return res;
        }

        public async Task<ShoppingCart> GetById(int shoppingCartId)
        {
            var res = await _context.ShoppingCarts.FindAsync(shoppingCartId);
            return res;
        }

        public async Task<bool> UpdateCart(int shoppingCartId, ShoppingCart cart)
        {
            var existingCart = await _context.ShoppingCarts.FindAsync(shoppingCartId);
            if (existingCart == null) return false;

            existingCart.ProductId = cart.ProductId;
            existingCart.Quantity = cart.Quantity;
            existingCart.CustomerId = cart.CustomerId;

            _context.ShoppingCarts.Update(existingCart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
