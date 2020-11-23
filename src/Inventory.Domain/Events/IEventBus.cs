using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Events
{
    public interface IEventBus
    {
        Task Publish(DomainEvent domainEvent);
    }
}