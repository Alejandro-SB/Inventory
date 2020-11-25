using System;

namespace Inventory.Domain.Events
{
    public abstract class DomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}