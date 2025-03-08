using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Repository.Service
{
    public class OrderDetailsRepository : IOrderDetailsRepo
    {
        private readonly AppDbContext _context;
        public OrderDetailsRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddOrderDetails(OrderDetails orderDetails)
        {
            await _context.OrderDetails.AddAsync(orderDetails);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderDetails(int orderDetailId)
        {
            var res = _context.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderDetailId);
            if (res == null) return false;
            _context.OrderDetails.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDetails>> GetAllOrderDetails()
        {
            IEnumerable<OrderDetails> res = _context.OrderDetails;
            if (res == null) return null;
            return res;
        }

        public async Task<OrderDetails> GetOrderDetailsById(int orderDetailId)
        {
            var res = await _context.OrderDetails.FirstOrDefaultAsync(y => y.OrderDetailId == orderDetailId);
            if (res == null) return null;
            return res;

        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderId(int orderId)
        {
            var res = await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
            return res;
        }

        public async Task<bool> UpdateOrderDetails(int orderDetailId, OrderDetails orderDetails)
        {
            var existingOrderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (existingOrderDetail == null) return false;

            existingOrderDetail.Quantity = orderDetails.Quantity;
            existingOrderDetail.Cost = orderDetails.Cost;
            existingOrderDetail.OrderId = orderDetails.OrderId;
            existingOrderDetail.ProductId = orderDetails.ProductId;

            _context.OrderDetails.Update(existingOrderDetail);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
