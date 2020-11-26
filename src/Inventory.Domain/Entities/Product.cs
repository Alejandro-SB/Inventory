using Inventory.Domain.Extensions;
using System;

namespace Inventory.Domain.Entities
{
    /// <summary>
    /// Class that represents a product in the inventory
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// The id of the product
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The date the product was deposited in the inventory
        /// </summary>
        public DateTime DepositDate { get; set; }
        /// <summary>
        /// The date of expiration
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// The type of the product
        /// </summary>
        public string? ProductType { get; set; }

        /// <summary>
        /// Creates an insance of the product
        /// </summary>
        /// <param name="name">The name of the product</param>
        public Product(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Creates an instance of the product
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <param name="name">The name of the product</param>
        public Product(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}