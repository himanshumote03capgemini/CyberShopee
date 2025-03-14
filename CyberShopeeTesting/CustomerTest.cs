using CyberShopee.Controllers;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CyberShopeeTesting;

[TestClass]
public class CustomerTest
{
    private Mock<ICustomerRepo> mockCustomerRepo;
    private CustomerController controller;

    [TestInitialize]
    public void Setup()
    {
        // Arrange - Initialize mock repository and controller before each test
        mockCustomerRepo = new Mock<ICustomerRepo>();
        controller = new CustomerController(mockCustomerRepo.Object);
    }

    // Test method to get all customers
    [TestMethod]
    public async Task GetAllCustomers_WithExistingCustomers_ReturnsOk()
    {
        // Arrange - Mock repository method
        var expectedCustomers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "John Doe", Email = "john@example.com" },
                new Customer { CustomerId = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };
        mockCustomerRepo.Setup(repo => repo.GetAllCustomers()).ReturnsAsync(expectedCustomers);

        // Act - Call the controller method
        var result = await controller.GetAllCustomers();

        // Assert - Validate the response
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(expectedCustomers, okResult.Value);
    }

    // Test method to get a customer by ID 
    [TestMethod]
    public async Task GetCustomerById_WithValidId_ReturnsOk()
    {
        // Arrange - Mock repository method
        var customer = new Customer { CustomerId = 1, Name = "John Doe", Email = "john@example.com" };
        mockCustomerRepo.Setup(repo => repo.GetCustomerById(1)).ReturnsAsync(customer);

        // Act - Call the controller method
        var result = await controller.GetCustomerById(1);

        // Assert - Validate the response
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(customer, okResult.Value);
    }

    [TestMethod]
    public async Task GetCustomerById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        mockCustomerRepo.Setup(repo => repo.GetCustomerById(99)).ReturnsAsync((Customer)null);

        // Act 
        var result = await controller.GetCustomerById(99);

        // Assert 
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
    }

    // Test method to add a customer 
    [TestMethod]
    public async Task AddCustomer_WithValidCustomer_ReturnsOk()
    {
        // Arrange 
        var newCustomer = new Customer { Name = "John Doe", Email = "john@example.com" };
        mockCustomerRepo.Setup(repo => repo.AddCustomer(newCustomer)).ReturnsAsync(true);

        // Act
        var result = await controller.AddCustomer(newCustomer);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
    }

    // Test method to add a customer
    [TestMethod]
    public async Task AddCustomer_WithInvalidCustomer_ReturnsBadRequest()
    {
        // Arrange 
        controller.ModelState.AddModelError("Email", "Required");

        // Act
        var result = await controller.AddCustomer(new Customer());

        // Assert 
        Assert.IsInstanceOfType<BadRequestObjectResult>(result);
    }

    // Test method to update a customer
    [TestMethod]
    public async Task UpdateCustomer_WithValidData_ReturnsOk()
    {
        // Arrange
        var updatedCustomer = new Customer { Name = "Updated Name", Email = "updated@example.com" };
        mockCustomerRepo.Setup(repo => repo.UpdateCustomer(1, updatedCustomer)).ReturnsAsync(true);

        // Act 
        var result = await controller.UpdateCustomer(1, updatedCustomer);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
    }

    [TestMethod]
    public async Task UpdateCustomer_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange 
        controller.ModelState.AddModelError("Email", "Required");

        // Act 
        var result = await controller.UpdateCustomer(1, new Customer());

        // Assert 
        Assert.IsInstanceOfType<BadRequestObjectResult>(result);
    }

    // Test method to delete a customer
    [TestMethod]
    public async Task DeleteCustomer_WithValidId_ReturnsOk()
    {
        // Arrange 
        mockCustomerRepo.Setup(repo => repo.DeleteCustomer(1)).ReturnsAsync(true);

        // Act 
        var result = await controller.DeleteCustomer(1);

        // Assert 
        Assert.IsInstanceOfType<OkObjectResult>(result);
    }

    [TestMethod]
    public async Task DeleteCustomer_WithInvalidId_ReturnsNotFound()
    {
        // Arrange 
        mockCustomerRepo.Setup(repo => repo.DeleteCustomer(99)).ReturnsAsync(false);

        // Act 
        var result = await controller.DeleteCustomer(99);

        // Assert
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
    }

    // Test method to get a customer by email
    [TestMethod]
    public async Task GetCustomerByEmail_WithValidEmail_ReturnsOk()
    {
        // Arrange 
        var customer = new Customer { CustomerId = 1, Name = "John Doe", Email = "john@example.com" };
        mockCustomerRepo.Setup(repo => repo.GetCustomerByEmail("john@example.com")).ReturnsAsync(customer);

        // Act 
        var result = await controller.GetCustomerByEmail("john@example.com");

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(customer, okResult.Value);
    }

    // Test method to search customers by name 
    [TestMethod]
    public async Task SearchCustomers_WithExistingName_ReturnsOk()
    {
        // Arrange
        var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "John Doe", Email = "john@example.com" }
            };
        mockCustomerRepo.Setup(repo => repo.SearchCustomers("John")).ReturnsAsync(customers);

        // Act 
        var result = await controller.SearchCustomers("John");

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
    }

    // Test method to get top customers by order count
    [TestMethod]
    public async Task GetTopCustomersByOrderCount_WithValidCount_ReturnsOk()
    {
        // Arrange 
        var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "John Doe", Email = "john@example.com" }
            };
        mockCustomerRepo.Setup(repo => repo.GetTopCustomersByOrderCount(2)).ReturnsAsync(customers);

        // Act 
        var result = await controller.GetTopCustomersByOrderCount(2);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
    }
}
