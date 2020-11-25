using Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface IProductRepository
    {
        ValueTask<Product?> GetByIdAsync(int id);
        Task<Product?> GetByNameAsync(string name);
        Task<List<Product>> GetExpiredProductsAsync();
        Task<List<Product>> GetAllProducts();
        Product AddProduct(Product product);
        Product DeleteProduct(Product product);
    }
}