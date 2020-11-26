using Inventory.Domain.Entities;
using Inventory.Domain.Persistence;
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
        private readonly IAuditUser _user;

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options, IAuditUser user) : base(options)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
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
            var entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity is BaseEntity).Select(x => (BaseEntity)x.Entity).ToList();
            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified && x.Entity is BaseEntity).Select(x => (BaseEntity)x.Entity).ToList();

            foreach (var entity in entities)
            {
                entity.CreationDate = DateTime.UtcNow;
                entity.CreationBy = _user.Username;
                entity.ModificationDate = DateTime.UtcNow;
                entity.ModificationBy = _user.Username;
            }

            foreach(var entity in modifiedEntities)
            {
                entity.ModificationDate = DateTime.UtcNow;
                entity.ModificationBy = _user.Username;
            }
        }
    }
}