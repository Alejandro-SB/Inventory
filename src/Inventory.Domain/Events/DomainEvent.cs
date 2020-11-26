using System;

namespace Inventory.Domain.Events
{
    /// <summary>
    /// Represents an abstract event
    /// </summary>
    public abstract class DomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}