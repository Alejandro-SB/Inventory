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
        /// <summary>
        /// List of all the events the entity should propagate
        /// </summary>
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        /// <summary>
        /// The date the entity was created
        /// </summary>
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// The user who created the entity
        /// </summary>
        public string? CreationBy { get; set; }
        /// <summary>
        /// The date the entity was modified
        /// </summary>
        public DateTime? ModificationDate { get; set; }
        /// <summary>
        /// The user who modified the entity
        /// </summary>
        public string? ModificationBy { get; set; }

        /// <summary>
        /// The list of the events the entity should propagate
        /// </summary>
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        /// <summary>
        /// Adds a new event to the queue
        /// </summary>
        /// <param name="domainEvent">The event to add</param>
        public void AddEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        /// <summary>
        /// Clears the list of events
        /// </summary>
        public void ClearEvents() => _domainEvents.Clear();
    }
}