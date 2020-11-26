using Inventory.Domain.Extensions;
using System;

namespace Inventory.API.Authentication.Login
{
    /// <summary>
    /// Class that represents an user of the application
    /// </summary>
    public class InventoryUser
    {
        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The id of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Creates an instance of the InventoryUser class
        /// </summary>
        /// <param name="name">The name of the user</param>
        /// <param name="email">The email of the user</param>
        /// <param name="userId">The id of the user</param>
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