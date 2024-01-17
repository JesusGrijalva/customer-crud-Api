using CrudCustomer.Controllers;
using CrudCustomer.Models;
using CrudCustomer.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CrudCustomer.Tests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkObjectResult_WithListOfCustomers()
        {
            // Arrange
            _mockCustomerService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<Customer>());

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<Customer>>();
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsOkObjectResult_WithListOfCustomers()
        {
            // Arrange
            int validId = 1;
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.GetByIdAsync(validId)).ReturnsAsync(this.CustomerMock());

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.GetByIdAsync(validId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<Customer>().Which.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsBadRequestObjectResult_WithListOfCustomers()
        {
            // Arrange
            int invalidId = -1;
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.GetByIdAsync(invalidId)).ReturnsAsync(this.CustomerMock());

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.GetByIdAsync(invalidId);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateAsync_WithValidCustomer_ReturnsOkObjectResult_WithListOfCustomers()
        {
            // Arrange
            var validCustomer = this.CustomerMock();
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.CreateAsync(validCustomer)).ReturnsAsync(validCustomer);

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.CreateAsync(validCustomer);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<Customer>().Which.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidCustomer_ReturnsBadRequestObjectResult_WithListOfCustomers()
        {
            // Arrange
            var validCustomer = new Customer();
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.CreateAsync(validCustomer)).ReturnsAsync(validCustomer);

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.CreateAsync(validCustomer);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateAsync_WithValidCustomer_ReturnsOkObjectResult_WithListOfCustomers()
        {
            // Arrange
            var validCustomer = this.CustomerMock();
            validCustomer.FirstName = "test2";
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.UpdateAsync(validCustomer)).ReturnsAsync(validCustomer);

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.UpdateAsync(validCustomer);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<Customer>().Which.FirstName.Should().BeEquivalentTo("test2");
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidCustomer_ReturnsBadRequestObjectResult_WithListOfCustomers()
        {
            // Arrange
            var validCustomer = this.CustomerMock();
            validCustomer.Id = -1;
            validCustomer.FirstName = "test2";
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.UpdateAsync(validCustomer)).ReturnsAsync(validCustomer);

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.UpdateAsync(validCustomer);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteByIdAsync_WithValidId_ReturnsOkObjectResult_WithListOfCustomers()
        {
            // Arrange
            int validId = 1;
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.DeleteAsync(validId)).ReturnsAsync(true);

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.DeleteByIdAsync(validId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<bool>();
        }

        [Fact]
        public async Task DeleteByIdAsync_WithInvalidId_ReturnsBadRequestObjectResult_WithListOfCustomers()
        {
            // Arrange
            int invalidId = -1;
            var _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(service => service.DeleteAsync(invalidId)).ReturnsAsync(true);

            var controller = new CustomerController(_mockCustomerService.Object);

            // Act
            var result = await controller.DeleteByIdAsync(invalidId);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        private Customer CustomerMock()
        {
           return new Customer { Id = 1, FirstName = "test", LastName = "perez", Email = "test@email.com", Created = System.DateTimeOffset.Now, Updated = System.DateTimeOffset.Now };
        }
    }
}
