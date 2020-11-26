using Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    /// <summary>
    /// Interface to access product information
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Returns the product by the id given
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <returns>The product or null if not found</returns>
        ValueTask<Product?> GetByIdAsync(int id);
        /// <summary>
        /// Returns the product matching the given name
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <returns>The product or null if not found</returns>
        Task<Product?> GetByNameAsync(string name);
        /// <summary>
        /// Returns the list of expired products
        /// </summary>
        /// <returns>A list with all the expired products</returns>
        Task<List<Product>> GetExpiredProductsAsync();
        /// <summary>
        /// Returns all the products
        /// </summary>
        /// <returns>A list with the products</returns>
        Task<List<Product>> GetAllProducts();
        /// <summary>
        /// Adds a product to the database
        /// </summary>
        /// <param name="product">The product to add</param>
        /// <returns>The product to be added</returns>
        Product AddProduct(Product product);
        /// <summary>
        /// Deletes a product from the database
        /// </summary>
        /// <param name="product">The product to delete</param>
        /// <returns>The product to be deleted</returns>
        Product DeleteProduct(Product product);
    }
}