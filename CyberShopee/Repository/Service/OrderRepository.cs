using System.Collections.Generic;
using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Repository.Service
{
    public class OrderRepository : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) { _context = context; }
        public async Task<bool> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            var res = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
            if (res == null) return false;
            _context.Orders.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            IEnumerable<Order> res = _context.Orders;
            if (res == null) return null;
            return res;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var res = await _context.Orders.FirstOrDefaultAsync(y => y.OrderId == orderId);
            if (res == null) return null;
            return res;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            IEnumerable<Order> res = await _context.Orders.Where(y => y.CustomerId == customerId).ToListAsync();
            if (res == null) return null;
            return res;
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            var res = await _context.Orders
                        .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                        .ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatus(string status)
        {
            var res = await _context.Orders
                        .Where(o => o.Status.ToLower() == status.ToLower())
                        .ToListAsync();
            return res;
        }

        public async Task<bool> UpdateOrder(int orderId, string status)
        {
            var existingOrder = await _context.Orders.FindAsync(orderId);
            if (existingOrder == null) return false;

            existingOrder.Status = status;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();

            return true;        
        }
    }
}
