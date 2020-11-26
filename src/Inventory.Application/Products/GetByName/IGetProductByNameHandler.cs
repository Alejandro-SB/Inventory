using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.GetByName
{
    /// <summary>
    /// Use case to manage product retrieval by name
    /// </summary>
    public interface IGetProductByNameHandler : IUseCaseHandler<GetProductByNameRequest, GetProductByNameResponse>
    {
    }
}