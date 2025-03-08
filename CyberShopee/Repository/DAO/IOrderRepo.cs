using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrders();

        Task<Order> GetOrderById(int orderId);

        Task<bool> AddOrder(Order order);

        Task<bool> UpdateOrder(int orderId, string status);

        Task<bool> DeleteOrder(int orderId);

        Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);

        Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);

        Task<IEnumerable<Order>> GetOrdersByStatus(string status); 
    }
}
