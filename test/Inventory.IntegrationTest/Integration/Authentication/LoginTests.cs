using Inventory.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Test.Integration.Authentication
{
    public class LoginTests
    {
        private const string _loginUrl = "Authentication/Authenticate";

        [Fact]
        public async Task When_Model_Is_Incomplete_Returns_Bad_Request()
        {
            //Arrange
            using var sut = new WebApplicationFactory<Startup>();
            using var client = sut.CreateClient();
            const string emptyJson = "{}";
            const string emptyUsernameJson = "{ \"Password\": \"123456\" }";
            const string emptyPasswordJson = "{ \"Username\": \"admin\" }";
            var emptyContent = new StringContent(emptyJson, Encoding.UTF8, "application/json");
            var emptyUsernameContent = new StringContent(emptyUsernameJson, Encoding.UTF8, "application/json");
            var emptyPasswordContent = new StringContent(emptyPasswordJson, Encoding.UTF8, "application/json");

            //Act
            var emptyResponse = await client.PostAsync(_loginUrl, emptyContent);
            var emptyUsernameResponse = await client.PostAsync(_loginUrl, emptyUsernameContent);
            var emptyPasswordResponse = await client.PostAsync(_loginUrl, emptyPasswordContent);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, emptyResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, emptyUsernameResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, emptyPasswordResponse.StatusCode);
        }

        [Fact]
        public async Task When_Username_And_Password_Are_Correct_Returns_Token()
        {
            //Arrange
            using var sut = new WebApplicationFactory<Startup>();
            using var client = sut.CreateClient();
            const string json = "{ \"Username\": \"admin\", \"Password\": \"123456\" }";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(_loginUrl, content);

            //Assert
            var token = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            Assert.NotNull(token);
            Assert.True(token.Length > 0);
        }
    }
}