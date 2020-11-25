using System.Threading.Tasks;

namespace Inventory.Domain.Events
{
    public interface IEventBus
    {
        Task Publish(DomainEvent domainEvent);
    }
}