using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Inventory.Web.Models;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
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