using System.Net;
using System.Net.Http;

namespace Inventory.Web.Api
{

    public static class ApiHelpers
    {
        public static HttpRequestMessage GetAuthenticatedMessage(string token, HttpMethod method, string uri)
        {
            var message = new HttpRequestMessage(method, uri);
            message.Headers.Add("Authorization", $"Bearer {token}");

            return message;
        }

        public static void EnsureAuthenticationSuccessful(this HttpResponseMessage response)
        {
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ApiAuthenticationException();
            }
        }
    }
}