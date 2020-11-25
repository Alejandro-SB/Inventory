using Inventory.Domain.Events;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Entities
{
    /// <summary>
    /// Base entity with audit data
    /// </summary>
    public abstract class BaseEntity
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public DateTime? CreationDate { get; set; }
        public string? CreationBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModificationBy { get; set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void AddEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearEvents() => _domainEvents.Clear();
    }
}