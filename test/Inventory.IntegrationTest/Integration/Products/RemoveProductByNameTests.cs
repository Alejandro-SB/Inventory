using Inventory.API;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.IntegrationTest.Integration.Products
{
    public class RemoveProductByNameTests : BypassAuthenticationTestBase
    {
        public RemoveProductByNameTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        protected override void SeedDatabase(InventoryDbContext context)
        {
            context.Products.Add(new Product("existing"));
        }

        [Fact]
        public async Task When_Product_Does_Not_Exist_Returns_NotFound()
        {
            //Arrange
            const string url = "Product/nonexisting";
            //Act
            var response = await Client.DeleteAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task When_Product_Exists_Returns_OK()
        {
            //Arrange
            const string url = "Product/existing";

            //Act
            var response = await Client.DeleteAsync(url);

            //Asert
            response.EnsureSuccessStatusCode();
        }
    }
}