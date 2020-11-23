using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Events
{
    public abstract class DomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}