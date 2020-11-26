using Inventory.API.Authentication.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventory.API.Authentication
{
    /// <summary>
    /// Controller to authenticate users
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Property that holds the configuration of the application
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Creates an instance of the AuthenticationController class
        /// </summary>
        /// <param name="configuration">The configuration of the application</param>
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="model">The data of the user</param>
        /// <returns>The token of the user</returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            if(model.Username == "admin" && model.Password == "123456")
            {
                var user = new InventoryUser("admin", "admin@inventory.test.dev", Guid.Empty.ToString());

                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId),
                    new Claim(ClaimTypes.Name, user.UserId),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var identity = new ClaimsIdentity(claims);

                var securityDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddHours(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(securityDescriptor);

                return Ok(handler.WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}