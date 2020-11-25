using Inventory.Domain.Extensions;
using System;

namespace Inventory.Domain.Entities
{
    /// <summary>
    /// Class that represents a product in the inventory
    /// </summary>
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? ProductType { get; set; }

        public Product(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public Product(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}