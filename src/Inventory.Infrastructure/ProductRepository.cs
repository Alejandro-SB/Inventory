using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ProductRepository(InventoryDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        /// <inheritdoc/>
        public ValueTask<Product?> GetByIdAsync(int id)
        {
            return _context.FindAsync<Product?>(id);
        }

        /// <inheritdoc/>
        public Task<Product?> GetByNameAsync(string name)
        {
            return _context.Set<Product?>().FirstOrDefaultAsync(x => x!.Name == name);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetExpiredProductsAsync()
        {
            var today = _dateTimeProvider.UtcNow;

            return _context.Set<Product>().Where(x => x.ExpirationDate < today).ToListAsync();
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetAllProducts()
        {
            return _context.Set<Product>().ToListAsync();
        }

        /// <inheritdoc/>
        public Product AddProduct(Product product)
        {
            return _context.Add(product).Entity;
        }

        /// <inheritdoc/>
        public Product DeleteProduct(Product product)
        {
            return _context.Remove(product).Entity;
        }
    }
}