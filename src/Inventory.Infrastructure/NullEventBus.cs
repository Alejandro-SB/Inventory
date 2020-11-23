using Inventory.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public class NullEventBus : IEventBus
    {
        private readonly ILogger<NullEventBus> _logger;

        public NullEventBus(ILogger<NullEventBus> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Publish(DomainEvent domainEvent)
        {
            string eventName = domainEvent.GetType().Name;
            Guid eventId = domainEvent.Id;
            DateTime creationDate = domainEvent.CreationDate;

            _logger.LogDebug($"Raised event {eventName} with Id = {eventId} created at {creationDate.ToString("dd/MM/yyyy")}");

            return Task.CompletedTask;
        }
    }
}