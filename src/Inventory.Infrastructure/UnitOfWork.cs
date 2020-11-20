using Inventory.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}