using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;

namespace CyberShopee.Repository.Service
{
    public class OrderRepository : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) { _context = context; }
        public async Task<bool> AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException(); 
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrder(int orderId, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
