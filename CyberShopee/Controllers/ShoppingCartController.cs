using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepo _repository;
        public ShoppingCartController(IShoppingCartRepo repository)
        {
            _repository = repository;
        }

        // Get all shopping cart items
        [HttpGet]
        public async Task<IActionResult> GetAllShoppingCart()
        {
            var cartItems = await _repository.GetAllShoppingCart();
            return Ok(cartItems);
        }

        // Get shopping cart item by ID
        [HttpGet("{shoppingCartId}")]
        public async Task<IActionResult> GetById(int shoppingCartId)
        {
            var cartItem = await _repository.GetById(shoppingCartId);
            if (cartItem == null) return NotFound("Cart item not found.");
            return Ok(cartItem);
        }

        // Add item to cart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] ShoppingCart cart)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid shopping cart data.");

            var result = await _repository.AddToCart(cart);
            if (!result) return StatusCode(500, "Failed to add item to cart.");

            return Ok("Item added to cart successfully.");
        }

        // Update cart item
        [HttpPut("{shoppingCartId}")]
        public async Task<IActionResult> UpdateCart(int shoppingCartId, [FromBody] ShoppingCart cart)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid shopping cart data.");

            var result = await _repository.UpdateCart(shoppingCartId, cart);
            if (!result) return NotFound("Cart item not found or update failed.");

            return Ok("Cart item updated successfully.");
        }

        // Delete cart item
        [HttpDelete("{shoppingCartId}")]
        public async Task<IActionResult> DeleteFromCart(int shoppingCartId)
        {
            var result = await _repository.DeleteFromCart(shoppingCartId);
            if (!result) return NotFound("Cart item not found or delete failed.");

            return Ok("Cart item deleted successfully.");
        }

        // Get shopping cart items by customer ID
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var cartItems = await _repository.GetByCustomerId(customerId);
            return Ok(cartItems);
        }
    }
}
