using Inventory.Domain.Events;
using System;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    /// <summary>
    /// Event that represents that a product has expired
    /// </summary>
    public class ProductExpiredEvent : DomainEvent
    {
        /// <summary>
        /// The name of the product that has expired
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The product expiration date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        public ProductExpiredEvent(string name, DateTime expirationDate)
        {
            Name = name;
            ExpirationDate = expirationDate;
        }
    }
}