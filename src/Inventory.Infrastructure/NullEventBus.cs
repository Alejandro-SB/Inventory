using Inventory.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    /// <summary>
    /// Represents an abstract event bus
    /// </summary>
    public class NullEventBus : IEventBus
    {
        private readonly ILogger<NullEventBus> _logger;

        public NullEventBus(ILogger<NullEventBus> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Mocks publishing by logging the operation requested
        /// </summary>
        /// <param name="domainEvent">The event to publish</param>
        /// <returns></returns>
        public Task Publish(DomainEvent domainEvent)
        {
            string eventName = domainEvent.GetType().Name;
            Guid eventId = domainEvent.Id;
            DateTime creationDate = domainEvent.CreationDate;

            _logger.LogDebug($"Raised event {eventName} with Id = \"{eventId}\" created at {creationDate:dd/MM/yyyy}");

            return Task.CompletedTask;
        }
    }
}