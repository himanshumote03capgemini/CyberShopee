using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _repository;
        public CustomerController(ICustomerRepo repository)
        {
            _repository = repository;
        }

        // Get all customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _repository.GetAllCustomers();
            return Ok(customers);
        }

        // Get customer by ID
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _repository.GetCustomerById(customerId);
            if (customer == null) return NotFound("Customer not found.");
            return Ok(customer);
        }

        // Add a new customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid) 
            {
                var result = await _repository.AddCustomer(customer);
                if (!result) return StatusCode(500, "Failed to add customer.");

                return Ok("Customer added successfully.");
            }
            return BadRequest("Invalid customer data.");
        }

        // Update customer
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateCustomer(customerId, customer);
                if (!result) return NotFound("Customer not found or update failed.");

                return Ok("Customer updated successfully.");
            }
            return BadRequest("Invalid customer data.");
        }

        // Delete customer
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var result = await _repository.DeleteCustomer(customerId);
            if (!result) return NotFound("Customer not found or delete failed.");

            return Ok("Customer deleted successfully.");
        }

        // Get customer by email
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var customer = await _repository.GetCustomerByEmail(email);
            if (customer == null) return NotFound("Customer not found.");
            return Ok(customer);
        }

        // Search customers by name
        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchCustomers(string name)
        {
            var customers = await _repository.SearchCustomers(name);
            if (customers == null) return NotFound("Customer not found.");
            return Ok(customers);
        }

        // Get customers by registration date range
        //[HttpGet("date-range")]
        //public async Task<IActionResult> GetCustomersByRegistrationDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        //{
        //    var customers = await _repository.GetCustomersByRegistrationDateRange(startDate, endDate);
        //    return Ok(customers);
        //}


        // Get top customers by order count
        [HttpGet("top/{count}")]
        public async Task<IActionResult> GetTopCustomersByOrderCount(int count)
        {
            var customers = await _repository.GetTopCustomersByOrderCount(count);
            if (customers == null) return NotFound("Customer not found.");
            return Ok(customers);
        }
    }
}
