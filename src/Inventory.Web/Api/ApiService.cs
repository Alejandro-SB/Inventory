using Inventory.Web.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Inventory.Web.Api
{
    /// <summary>
    /// Class that represents an abstract api interaction
    /// </summary>
    public class ApiService
    {
        private readonly string _baseUrl;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        /// <summary>
        /// Logs the user into the application
        /// </summary>
        /// <param name="username">The name of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>The token or null if not valid</returns>
        public async Task<string> LoginAsync(string username, string password)
        {
            var url = _baseUrl + "Authentication/Authenticate";

            var loginDto = new ApiLoginDto
            {
                Username = username,
                Password = password
            };

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = await response.Content.ReadAsStringAsync();

                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="token">The token to access the API</param>
        /// <param name="productName">The name of the product</param>
        /// <param name="expirationDate">The date of expiration of the product</param>
        /// <param name="productType">The type of the product</param>
        /// <returns>The errror if any or null if ok</returns>
        public async Task<string> CreateProductAsync(string token, string productName, DateTime? expirationDate = null, string productType = null)
        {
            var url = _baseUrl + "Product";

            var message = ApiHelpers.GetAuthenticatedMessage(token, HttpMethod.Post, url);

            var product = new ProductDto
            {
                Name = productName,
                ExpirationDate = expirationDate,
                ProductType = productType
            };

            message.Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(message);

                response.EnsureAuthenticationSuccessful();

                string result;

                if(response.StatusCode == HttpStatusCode.Created)
                {
                    result = null;
                }
                else if((int)response.StatusCode == 422) //Unprocesable entity
                {
                    var reasonJson = await response.Content.ReadAsStringAsync();

                    var reason = JObject.Parse(reasonJson);
                    var validationReason = reason.SelectToken("errors.Validations[0]");

                    if(validationReason != null && validationReason.Value<string>().Length > 0)
                    {
                        result = validationReason.Value<string>();
                    }
                    else
                    {
                        result = "An error ocurred while processing your request";
                    }
                }
                else
                {
                    result = "An error ocurred while processing your request";
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a product by name
        /// </summary>
        /// <param name="token">The token to access the API</param>
        /// <param name="productName">The name of the product</param>
        /// <returns>The product or null if not found</returns>
        public async Task<ProductDto> GetProductByNameAsync(string token, string productName)
        {
            var url = $"{_baseUrl}Product/{productName}";

            var message = ApiHelpers.GetAuthenticatedMessage(token, HttpMethod.Get, url);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(message);

                response.EnsureAuthenticationSuccessful();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var product = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ProductDto>(product);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns all the products
        /// </summary>
        /// <param name="token">The token to access the API</param>
        /// <returns>A list with all the products</returns>
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string token)
        {
            var url = $"{_baseUrl}Product";

            var message = ApiHelpers.GetAuthenticatedMessage(token, HttpMethod.Get, url);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(message);

                response.EnsureAuthenticationSuccessful();

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var products = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<ProductDto>>(products);
                }
                else
                {
                    return new List<ProductDto>();
                }
            }
        }

        /// <summary>
        /// Deletes a product by name
        /// </summary>
        /// <param name="token">The token to access the API</param>
        /// <param name="productName">The name of the product</param>
        /// <returns>The product deleted</returns>
        public Task<ProductDto> DeleteProductAsync(string token, string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName));
            }

            return DeleteProduct(token, productName);
        }

        /// <summary>
        /// Deletes the product
        /// </summary>
        /// <param name="token">The token to access the API</param>
        /// <param name="productName">The name of the product</param>
        /// <returns>The product deleted</returns>
        private async Task<ProductDto> DeleteProduct(string token, string productName)
        {
            var url = $"{_baseUrl}Product/{HttpUtility.UrlEncode(productName)}";

            var message = ApiHelpers.GetAuthenticatedMessage(token, HttpMethod.Delete, url);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(message);

                response.EnsureAuthenticationSuccessful();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductDto>(content);

                    return product;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}