using Inventory.Application.Products.NotifyExpiredProducts;
using Inventory.Domain.Entities;
using Inventory.Domain.Events;
using Inventory.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Test.Application.Products
{
    public class NotifyExpiredProductsTests
    {
        /// <summary>
        /// Ensures that when there are no expired products, no events are published
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task When_There_Are_No_Expired_Products_Returns_0()
        {
            //Arrange
            var request = new NotifyExpiredProductsRequest();

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.GetExpiredProductsAsync()).Returns(() => Task.FromResult(new List<Product>()));

            var eventBus = new Mock<IEventBus>();

            var handler = new NotifyExpiredProductsHandler(productRepository.Object, eventBus.Object);

            //Act
            var response = await handler.Handle(request);

            //Assert
            eventBus.Verify(x => x.Publish(It.IsAny<ProductExpiredEvent>()), Times.Never);
            Assert.Equal(0, response.ExpiredProducts);
        }

        /// <summary>
        /// Ensures that when there are products with past expiration date, notifies expiration
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task When_There_Are_Expired_Products_Notifies_Then_And_Returns_Total_Products_Expired()
        {
            //Arrange
            var request = new NotifyExpiredProductsRequest();
            var todayDate = new DateTime(2020, 1, 1);
            var pastDate = todayDate.AddDays(-3);

            var expiredProducts = GetProductsWithExpirationDate(pastDate);

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.GetExpiredProductsAsync()).Returns(() => Task.FromResult(expiredProducts));

            var eventBus = new Mock<IEventBus>();

            var handler = new NotifyExpiredProductsHandler(productRepository.Object, eventBus.Object);

            //Act
            var response = await handler.Handle(request);

            //Assert
            eventBus.Verify(x => x.Publish(It.IsAny<ProductExpiredEvent>()), Times.Exactly(expiredProducts.Count)); //Verify each expiration is called
            Assert.Equal(expiredProducts.Count, response.ExpiredProducts);
        }

        /// <summary>
        /// Creates a random number of products
        /// </summary>
        /// <returns>A list with a random number of products</returns>
        private List<Product> GetProductsWithExpirationDate(DateTime expirationDate)
        {
            int totalProducts = new Random().Next(3, 10);

            var products = new List<Product>();

            for(int i = 0; i < totalProducts; ++i)
            {
                products.Add(new Product(Guid.NewGuid().ToString())
                {
                    ExpirationDate = expirationDate
                });
            }

            return products;
        }
    }
}