using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.GetAllProducts
{
    public interface IGetAllProductsHandler : IUseCaseHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
    }
}