using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _repository;
        public OrderController(IOrderRepo repository)
        {
            _repository = repository;
        }

        // Get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _repository.GetAllOrders();
            return Ok(orders);
        }

        // Get order by ID
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _repository.GetOrderById(orderId);
            if (order == null) return NotFound("Order not found.");
            return Ok(order);
        }

        // Add a new order
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.AddOrder(order);
                if (!result) return StatusCode(500, "Failed to add order.");

                return Ok("Order added successfully.");
            }
            return BadRequest("Invalid order data.");           
        }

        // Update order status
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status)) return BadRequest("Invalid status.");

            var result = await _repository.UpdateOrder(orderId, status);
            if (!result) return NotFound("Order not found or update failed.");

            return Ok("Order updated successfully.");
        }

        // Delete an order
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _repository.DeleteOrder(orderId);
            if (!result) return NotFound("Order not found or delete failed.");

            return Ok("Order deleted successfully.");
        }

        // Get orders by customer ID
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _repository.GetOrdersByCustomerId(customerId);
            if (orders == null) return NotFound("Customer not found");
            return Ok(orders);
        }

        // Get orders by date range
        [HttpGet("date-range")]
        public async Task<IActionResult> GetOrdersByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var orders = await _repository.GetOrdersByDateRange(startDate, endDate);
            return Ok(orders);
        }

        // Get orders by status
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            var orders = await _repository.GetOrdersByStatus(status);
            if (orders == null) return NotFound("Orders not found");
            return Ok(orders);
        }
    }
}
