using Inventory.Application.Products.CreateProduct;
using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Entities;
using Inventory.Domain.Persistence;
using Inventory.Domain.Repositories;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Test.Application.Products
{
    public class CreateProductTests
    {
        [Fact]
        public void When_Null_Or_Empty_Name_Is_Not_Valid_In_Request_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new CreateProductRequest(null!));
            Assert.Throws<ArgumentNullException>(() => new CreateProductRequest(string.Empty));
        }

        [Fact]
        public void When_Name_Already_Exists_Throws()
        {
            //Arrange
            const string productName = "name";

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(x => x.GetActiveByNameAsync(It.Is<string>(x => x == productName)))
                .Returns(() => Task.FromResult<Product?>(new Product(productName)));

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.UtcNow)
                .Returns(DateTime.UtcNow);

            var handler = new CreateProductHandler(repositoryMock.Object, unitOfWorkMock.Object, dateTimeProvider.Object);
            var request = new CreateProductRequest(productName);

            //Assert
            Assert.ThrowsAsync<ProductAlreadyExistsException>(() => handler.Handle(request));
        }

        [Fact]
        public async Task When_Name_Does_Not_Exists_Inserts_And_Returns_Id()
        {
            //Arrange
            const string productName = "name";
            const int generatedId = 1;

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(x => x.GetActiveByNameAsync(It.Is<string>(x => x == productName)))
                .Returns(() => Task.FromResult<Product?>(null));
            //Assign simulated product ID
            repositoryMock.Setup(x => x.AddProduct(It.IsAny<Product>()))
                .Callback<Product>(product => product.Id = generatedId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

            var handler = new CreateProductHandler(repositoryMock.Object, unitOfWorkMock.Object, dateTimeProvider.Object);
            var request = new CreateProductRequest(productName);

            //Act
            var response = await handler.Handle(request);

            //Assert
            Assert.Equal(generatedId, response.Id);
        }
    }
}