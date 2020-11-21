using Inventory.API;
using Inventory.Infrastructure;
using Inventory.IntegrationTest.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using Xunit;

namespace Inventory.IntegrationTest.Integration
{
    public abstract class BypassAuthenticationTestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected HttpClient Client { get; }

        protected BypassAuthenticationTestBase(WebApplicationFactory<Startup> factory)
        {
            Client = factory.WithWebHostBuilder(builder => {
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
                        SeedDatabase(dbContext);
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

        protected virtual void SeedDatabase(InventoryDbContext context)
        {
        }
    }
}