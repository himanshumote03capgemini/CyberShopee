
using Moq;
using CyberShopee.Repository.DAO;
using CyberShopee.Models;
using CyberShopee.Repository.Service;
using CyberShopee.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CyberShopeeTesting;

[TestClass]
public class CategoryTest
{
    private Mock<ICategoryRepo> mockCategoryRepo;
    private List<Category> expectedCategory;

    [TestInitialize]
    public void Setup()
    {
        mockCategoryRepo = new Mock<ICategoryRepo>();
        expectedCategory = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Category 1", Description="Desc1" },
                new Category { CategoryId = 2, Name = "Category 2", Description="Desc2" }
            };
    }

    [TestMethod]
    public async Task GetCategory_WithExistingCategory_ReturnsOk()
    {
        // Arrange

        mockCategoryRepo.Setup(repo => repo.GetAllCategories())
                              .ReturnsAsync(expectedCategory); // Mocking the repository method
        var controller = new CategoryController(mockCategoryRepo.Object);
        // Act
        var result = await controller.GetAllCategories();

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(expectedCategory, okResult.Value);
    }

    [TestMethod]
    public async Task GetCategory_WithExistingCategoryId_ReturnsOk()
    {
        // Arrange
        int existingCategoryId = 1;
        var existingCategory = new Category { CategoryId = 1, Name = "Category 1", Description = "Desc1" };
        mockCategoryRepo.Setup(repo => repo.GetCategoryById(existingCategoryId))
                              .ReturnsAsync(existingCategory); // Mocking the repository method

        var controller = new CategoryController(mockCategoryRepo.Object);
        // Act
        var result = await controller.GetCategoryById(existingCategoryId);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
    }

    [TestMethod]
    public async Task GetCategory_WithNonExistingCategoryId_ReturnsNotFound()
    {
        // Arrange
        int nonExistingCategoryId = 999; // Assume a non-existing Category ID
        mockCategoryRepo.Setup(repo => repo.GetCategoryById(nonExistingCategoryId))
                              .ReturnsAsync((Category)null); // Mocking the repository method
        var controller = new CategoryController(mockCategoryRepo.Object);
        // Act
        var result = await controller.GetCategoryById(nonExistingCategoryId);

        // Assert
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
    }


    [TestMethod]
    public async Task CreateCategory_WithValidModel_ReturnsCreatedAtAction()
    {
        // Arrange
        var validCategory = new Category { CategoryId = 3, Name = "Category 3", Description = "Desc3" };

        mockCategoryRepo.Setup(repo => repo.AddCategory(validCategory)).ReturnsAsync(true);
        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.AddCategory(validCategory);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.AreEqual("Category added successfully.", okResult.Value); // Validate message
    }

    [TestMethod]
    public async Task AddCategory_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange
        var invalidCategory = new Category { CategoryId = 0, Name = "", Description = "" }; // Invalid data
        var controller = new CategoryController(mockCategoryRepo.Object);

        // Manually set model state to invalid
        controller.ModelState.AddModelError("Name", "Category Name is required");

        // Act
        var result = await controller.AddCategory(invalidCategory);

        // Assert
        Assert.IsInstanceOfType< BadRequestObjectResult>(result); // Expect BadRequestObjectResult

        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid category data.", badRequestResult.Value); // Validate error message
    }

    [TestMethod]
    public async Task UpdateCategory_WithValidModel_ReturnsOk()
    {
        // Arrange
        int categoryId = 1;
        var validCategory = new Category { CategoryId = 1, Name = "Updated Category", Description = "Updated Desc" };

        mockCategoryRepo.Setup(repo => repo.UpdateCategory(categoryId, validCategory))
                        .ReturnsAsync(true); // Simulate successful update

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.UpdateCategory(categoryId, validCategory);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual("Category updated successfully.", okResult.Value);
    }


    [TestMethod]
    public async Task UpdateCategory_WithNonExistingCategory_ReturnsNotFound()
    {
        // Arrange
        int categoryId = 999; // Assume non-existing category
        var categoryToUpdate = new Category { CategoryId = 999, Name = "Non-Existing", Description = "No Desc" };

        mockCategoryRepo.Setup(repo => repo.UpdateCategory(categoryId, categoryToUpdate))
                        .ReturnsAsync(false); // Simulate category not found

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.UpdateCategory(categoryId, categoryToUpdate);

        // Assert
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Category not found or update failed.", notFoundResult.Value);
    }

    [TestMethod]
    public async Task UpdateCategory_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange
        int categoryId = 1;
        var invalidCategory = new Category { CategoryId = 1, Name = "", Description = "" }; // Invalid model

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Manually invalidate model state
        controller.ModelState.AddModelError("Name", "Category Name is required");

        // Act
        var result = await controller.UpdateCategory(categoryId, invalidCategory);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid category data.", badRequestResult.Value);
    }


    // 
    [TestMethod]
    public async Task DeleteCategory_WithExistingCategory_ReturnsOk()
    {
        // Arrange
        int categoryId = 1;

        mockCategoryRepo.Setup(repo => repo.DeleteCategory(categoryId))
                        .ReturnsAsync(true); // Simulate successful deletion

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.DeleteCategory(categoryId);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual("Category deleted successfully.", okResult.Value);
    }

    [TestMethod]
    public async Task DeleteCategory_WithNonExistingCategory_ReturnsNotFound()
    {
        // Arrange
        int categoryId = 999; // Assume non-existing category

        mockCategoryRepo.Setup(repo => repo.DeleteCategory(categoryId))
                        .ReturnsAsync(false); // Simulate category not found

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.DeleteCategory(categoryId);

        // Assert
        Assert.IsInstanceOfType< NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Category not found or delete failed.", notFoundResult.Value);
    }

    [TestMethod]
    public async Task SearchByCategoryName_WithExistingCategory_ReturnsOk()
    {
        // Arrange
        string categoryName = "Shoes";
        var categories = new List<Category>
        {
            new Category { CategoryId = 1, Name = "Shoes", Description = "Footwear" }
        };

        mockCategoryRepo.Setup(repo => repo.SearchByCategoryName(categoryName))
                        .ReturnsAsync(categories); // Simulate found category

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.SearchByCategoryName(categoryName);

        // Assert
        Assert.IsInstanceOfType< OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(categories, okResult.Value);
    }

    [TestMethod]
    public async Task SearchByCategoryName_WithNonExistingCategory_ReturnsNotFound()
    {
        // Arrange
        string categoryName = "NonExistentCategory";

        mockCategoryRepo.Setup(repo => repo.SearchByCategoryName(categoryName))
                        .ReturnsAsync((List<Category>)null); // Simulate no category found

        var controller = new CategoryController(mockCategoryRepo.Object);

        // Act
        var result = await controller.SearchByCategoryName(categoryName);

        // Assert
        Assert.IsInstanceOfType< NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Category not found.", notFoundResult.Value);
    }






}
