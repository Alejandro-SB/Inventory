using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Authentication.Login
{
    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}