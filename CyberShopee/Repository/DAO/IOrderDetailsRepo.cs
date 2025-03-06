using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface IOrderDetailsRepo
    {
        Task<IEnumerable<OrderDetails>> GetAllOrderDetails();

        Task<OrderDetails> GetOrderDetailsById(int orderDetailId);

        Task<bool> AddOrderDetails(OrderDetails orderDetails);

        Task<bool> UpdateOrderDetails(int orderDetailId, OrderDetails orderDetails);

        Task<bool> DeleteOrderDetails(int orderDetailId);

        Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderId(int orderId);
    }
}
