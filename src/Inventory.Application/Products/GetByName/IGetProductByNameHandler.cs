using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.GetByName
{
    public interface IGetProductByNameHandler : IUseCaseHandler<GetProductByNameRequest, GetProductByNameResponse>
    {
    }
}