using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.IntegrationTest.Helpers
{
    public class StubPolicyEvaluator : IPolicyEvaluator
    {
        public virtual Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            string scheme = "TestScheme";
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, Guid.Empty.ToString()),
                new Claim(ClaimTypes.Email, "admin@inventory.test.dev")
            }, scheme));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, new AuthenticationProperties(), scheme)));
        }

        public virtual Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
            AuthenticateResult authenticationResult, HttpContext context, object resource)
        {
            return Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}