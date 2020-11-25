using Inventory.Domain.Events;

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