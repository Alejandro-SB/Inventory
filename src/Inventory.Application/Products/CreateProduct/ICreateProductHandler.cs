using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.CreateProduct
{
    /// <summary>
    /// Use case to manage product creation
    /// </summary>
    public interface ICreateProductHandler : IUseCaseHandler<CreateProductRequest, CreateProductResponse>
    {
    }
}