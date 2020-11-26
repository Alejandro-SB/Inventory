using System.Net;
using System.Net.Http;

namespace Inventory.Web.Api
{
    /// <summary>
    /// Helper class to access the API
    /// </summary>
    public static class ApiHelpers
    {
        /// <summary>
        /// Returns a message ready to be authenticated against the API
        /// </summary>
        /// <param name="token">The token to access the API</param>
        /// <param name="method">The method for the call</param>
        /// <param name="uri">The uri of the resource</param>
        /// <returns>The message with the parameters supplied</returns>
        public static HttpRequestMessage GetAuthenticatedMessage(string token, HttpMethod method, string uri)
        {
            var message = new HttpRequestMessage(method, uri);
            message.Headers.Add("Authorization", $"Bearer {token}");

            return message;
        }

        /// <summary>
        /// Ensures that the response has not been rejected by an authentication problem. If unauthorized, throws ApiAuthenticationException
        /// </summary>
        /// <param name="response">The response to check</param>
        public static void EnsureAuthenticationSuccessful(this HttpResponseMessage response)
        {
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ApiAuthenticationException();
            }
        }
    }
}