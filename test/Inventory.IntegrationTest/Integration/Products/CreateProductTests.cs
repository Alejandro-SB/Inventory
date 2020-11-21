using Inventory.API;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Inventory.IntegrationTest.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.IntegrationTest.Integration.Products
{
    public class CreateProductTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateProductTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(builder => {
                builder.ConfigureServices(services =>
                {
                    var provider = new ServiceCollection()
                            .AddEntityFrameworkInMemoryDatabase()
                            .BuildServiceProvider();

                    services.RemoveAll(typeof(InventoryDbContext));
                    services.AddDbContext<InventoryDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDb");
                        options.UseInternalServiceProvider(provider);
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();

                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<InventoryDbContext>();

                    try
                    {
                        dbContext.Products.Add(new Product("existing"));
                        dbContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }).ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, StubPolicyEvaluator>();
                });
            }).CreateClient();
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
            var emptyResponse = await _client.PostAsync("Product", emptyContent);
            var emptyUsernameResponse = await _client.PostAsync("Product", emptyUsernameContent);

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
            var response = await _client.PostAsync("Product", content);

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
            var response = await _client.PostAsync("Product", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}