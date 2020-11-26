using Inventory.Domain.Events;

namespace Inventory.Application.Products.RemoveByName
{
    /// <summary>
    /// Event that represents that a product has been removed
    /// </summary>
    public class ProductRemovedEvent : DomainEvent
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name { get; }

        public ProductRemovedEvent(string name)
        {
            Name = name;
        }
    }
}