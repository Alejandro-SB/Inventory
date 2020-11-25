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
    public class ApiService
    {
        private readonly string _baseUrl;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public async Task<string> Login(string username, string password)
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

        public async Task<ProductDto> GetProductByName(string token, string productName)
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

        public async Task<IEnumerable<ProductDto>> GetAllProducts(string token)
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

        public Task<ProductDto> DeleteProductAsync(string token, string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName));
            }

            return DeleteProduct(token, productName);
        }

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