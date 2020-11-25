using Inventory.Domain.Extensions;
using System;

namespace Inventory.API.Authentication.Login
{
    public class InventoryUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        public InventoryUser(string name, string email, string userId)
        {
            if(name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (email.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (userId.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userId));
            }

            Name = name;
            Email = email;
            UserId = userId;
        }
    }
}