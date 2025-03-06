using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;

namespace CyberShopee.Repository.Service
{
    public class OrderDetailsRepository : IOrderDetailsRepo
    {
        private readonly AppDbContext _context;
        public OrderDetailsRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddOrderDetails(OrderDetails orderDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteOrderDetails(int orderDetailId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetails>> GetAllOrderDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetails> GetOrderDetailsById(int orderDetailId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrderDetails(int orderDetailId, OrderDetails orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}
