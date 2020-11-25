using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Configuration;

namespace Inventory.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = "AuthCookie",
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Passive,
                AuthenticationType = "JWT",
                AllowedAudiences = new[] { "Inventory.API" },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider("Inventory.API", ConfigurationManager.AppSettings["APIKey"])
                }
            });
        }
    }
}