using Inventory.Application.Products;
using Inventory.Application.Products.RemoveByName;
using Inventory.Domain.Entities;
using Inventory.Domain.Persistence;
using Inventory.Domain.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Test.Application.Products
{
    public class WithdrawProductTests
    {
        [Fact]
        public void When_The_Name_Is_Null_Or_Empty_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new RemoveProductByNameRequest(null!));
            Assert.Throws<ArgumentNullException>(() => new RemoveProductByNameRequest(string.Empty));
        }

        [Fact]
        public void When_The_Product_Does_Not_Exist_Throws()
        {
            //Arrange
            var request = new RemoveProductByNameRequest("name");
            var repository = new Mock<IProductRepository>();
            repository.Setup(x => x.GetByNameAsync(It.IsAny<string>())).Returns(() => Task.FromResult<Product?>(null));

            var unitOfWork = new Mock<IUnitOfWork>();

            var handler = new RemoveProductByNameHandler(repository.Object, unitOfWork.Object);

            //Assert
            Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(request));
        }

        [Fact]
        public async Task When_The_Product_Exists_Marks_It_As_Withdrawn_And_Raises_Event()
        {
            //Arrange
            const string productName = "name";
            DateTime withdrawalDate = new DateTime(2020, 1, 1);

            var product = new Product(productName);

            var request = new RemoveProductByNameRequest(productName);
            var repository = new Mock<IProductRepository>();
            repository.Setup(x => x.GetByNameAsync(It.IsAny<string>())).Returns(() => Task.FromResult<Product?>(product));

            var unitOfWork = new Mock<IUnitOfWork>();

            var handler = new RemoveProductByNameHandler(repository.Object, unitOfWork.Object);

            //Act
            var response = await handler.Handle(request);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(product, response.Product);
            Assert.Equal(1, product.DomainEvents.Count);
        }
    }
}