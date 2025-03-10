using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsRepo _repository;
        public OrderDetailsController(IOrderDetailsRepo repository)
        {
            _repository = repository;
        }

        // Get all order details
        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _repository.GetAllOrderDetails();
            return Ok(orderDetails);
        }

        // Get order details by ID
        [HttpGet("{orderDetailId}")]
        public async Task<IActionResult> GetOrderDetailsById(int orderDetailId)
        {
            var orderDetail = await _repository.GetOrderDetailsById(orderDetailId);
            if (orderDetail == null) return NotFound("Order detail not found.");
            return Ok(orderDetail);
        }

        // Add a new order detail
        [HttpPost]
        public async Task<IActionResult> AddOrderDetails([FromBody] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.AddOrderDetails(orderDetails);
                if (!result) return StatusCode(500, "Failed to add order detail.");

                return Ok("Order detail added successfully.");
            }
            return BadRequest("Invalid order details data.");
        }

        // Update order detail
        [HttpPut("{orderDetailId}")]
        public async Task<IActionResult> UpdateOrderDetails(int orderDetailId, [FromBody] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateOrderDetails(orderDetailId, orderDetails);
                if (!result) return NotFound("Order detail not found or update failed.");

                return Ok("Order detail updated successfully.");
            }
            return BadRequest("Invalid order details data.");
        }

        // Delete an order detail
        [HttpDelete("{orderDetailId}")]
        public async Task<IActionResult> DeleteOrderDetails(int orderDetailId)
        {
            var result = await _repository.DeleteOrderDetails(orderDetailId);
            if (!result) return NotFound("Order detail not found or delete failed.");

            return Ok("Order detail deleted successfully.");
        }

        // Get order details by order ID
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _repository.GetOrderDetailsByOrderId(orderId);
            if (orderDetails == null) return NotFound("Order detail not found.");
            return Ok(orderDetails);
        }
    }
}
