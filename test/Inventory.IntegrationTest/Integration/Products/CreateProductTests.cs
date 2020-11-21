using Inventory.API;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.IntegrationTest.Integration.Products
{
    public class CreateProductTests : BypassAuthenticationTestBase
    {
        public CreateProductTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        protected override void SeedDatabase(InventoryDbContext context)
        {
            context.Products.Add(new Product("existing"));
            context.SaveChanges();
        }

        [Fact]
        public async Task When_Request_Is_Invalid_Returns_BadRequest()
        {
            //Arrange
            const string emptyJson = "{}";
            const string emptyUsernameJson = @"{""Username"": """"}";
            var emptyContent = new StringContent(emptyJson, Encoding.UTF8, "application/json");
            var emptyUsernameContent = new StringContent(emptyUsernameJson, Encoding.UTF8, "application/json");

            //Act
            var emptyResponse = await Client.PostAsync("Product", emptyContent);
            var emptyUsernameResponse = await Client.PostAsync("Product", emptyUsernameContent);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, emptyResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, emptyUsernameResponse.StatusCode);
        }

        [Fact]
        public async Task When_Product_Already_Exists_Returns_Unprocessable_Entity()
        {
            //Arrange
            const string json = @"{""Name"": ""existing""}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await Client.PostAsync("Product", content);

            //Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async Task When_Product_Is_Valid_Returns_OK()
        {
            //Arrange
            string productName = Guid.NewGuid().ToString("N");
            string json = @"{""Name"": """ + productName + @"""}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await Client.PostAsync("Product", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}