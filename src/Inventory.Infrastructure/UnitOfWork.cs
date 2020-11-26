using Inventory.Domain.Entities;
using Inventory.Domain.Events;
using Inventory.Domain.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        private readonly IEventBus _eventBus;

        public UnitOfWork(InventoryDbContext context, IEventBus eventBus)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchEvents();

            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Dispatches all the pending events from the entities
        /// </summary>
        /// <returns></returns>
        private async Task DispatchEvents()
        {
            var entities = _context.ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents.Any());

            var domainEvents = entities.SelectMany(x => x.Entity.DomainEvents).ToList();

            foreach(var entity in entities)
            {
                entity.Entity.ClearEvents();
            }

            foreach(var ev in domainEvents)
            {
                await _eventBus.Publish(ev);
            }
        }
    }
}