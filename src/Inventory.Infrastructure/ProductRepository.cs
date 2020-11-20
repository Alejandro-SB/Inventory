using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ValueTask<Product?> GetByIdAsync(int id)
        {
            return _context.FindAsync<Product?>(id);
        }

        public Task<Product?> GetActiveByNameAsync(string name)
        {
            return _context.Set<Product?>().FirstOrDefaultAsync(x => x!.Name == name);
        }

        public Product AddProduct(Product product)
        {
            return _context.Add(product).Entity;
        }

        public Product DeleteProduct(Product product)
        {
            return _context.Remove(product).Entity;
        }
    }
}