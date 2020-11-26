using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.GetAllProducts
{
    /// <summary>
    /// Use case to manage product retrieval
    /// </summary>
    public interface IGetAllProductsHandler : IUseCaseHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
    }
}