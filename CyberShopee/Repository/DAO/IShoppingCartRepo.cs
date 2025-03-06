using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface IShoppingCartRepo
    {
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCart();
        Task<ShoppingCart> GetById(int shoppingCartId);
        Task<bool> AddToCart(ShoppingCart cart);
        Task<bool> UpdateCart(int shoppingCartId, ShoppingCart cart);
        Task<bool> DeleteFromCart(int shoppingCartId);
        Task<IEnumerable<ShoppingCart>> GetByCustomerId(int customerId);
    }
}
