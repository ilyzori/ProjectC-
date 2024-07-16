using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CustomApp.Controllers;
using CustomApp.Models;
using CustomApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace CustomApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetProducts())
                    .Returns(GetTestProducts());
            var controller = new HomeController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Category = "Category1", Price = 10 },
                new Product { Id = 2, Name = "Product2", Category = "Category2", Price = 20 }
            };
        }
    }
}
