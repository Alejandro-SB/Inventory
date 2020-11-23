using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface IProductRepository
    {
        ValueTask<Product?> GetByIdAsync(int id);
        Task<Product?> GetByNameAsync(string name);
        Task<List<Product>> GetExpiredProductsAsync();
        Product AddProduct(Product product);
        Product DeleteProduct(Product product);
    }
}