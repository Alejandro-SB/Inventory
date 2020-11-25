using Inventory.Domain.Entities;
using Inventory.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditForEntities();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditForEntities()
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity is BaseEntity).ToList();
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified && x.Entity is BaseEntity).ToList();

            foreach(var entry in addedEntries)
            {
                ((BaseEntity)entry.Entity).CreationDate = DateTime.UtcNow;
                ((BaseEntity)entry.Entity).ModificationDate = DateTime.UtcNow;
            }

            foreach(var entry in modifiedEntries)
            {
                ((BaseEntity)entry.Entity).ModificationDate = DateTime.UtcNow;
            }
        }
    }
}