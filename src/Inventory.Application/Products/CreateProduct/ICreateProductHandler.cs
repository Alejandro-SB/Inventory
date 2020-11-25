using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.CreateProduct
{
    public interface ICreateProductHandler : IUseCaseHandler<CreateProductRequest, CreateProductResponse>
    {
    }
}