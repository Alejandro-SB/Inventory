using Inventory.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.RemoveByName
{
    public class ProductRemovedEvent : DomainEvent
    {
        public string Name { get; }

        public ProductRemovedEvent(string name)
        {
            Name = name;
        }
    }
}