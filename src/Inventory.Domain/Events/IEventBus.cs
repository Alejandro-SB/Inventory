using System.Threading.Tasks;

namespace Inventory.Domain.Events
{
    /// <summary>
    /// Defines a generalized event bus
    /// </summary>
    public interface IEventBus
    {
        Task Publish(DomainEvent domainEvent);
    }
}