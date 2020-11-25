using Inventory.Domain.Events;
using System;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    public class ProductExpiredEvent : DomainEvent
    {
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ProductExpiredEvent(string name, DateTime expirationDate)
        {
            Name = name;
            ExpirationDate = expirationDate;
        }
    }
}