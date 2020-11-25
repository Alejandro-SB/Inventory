using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.RemoveByName
{
    public interface IRemoveProductByNameHandler : IUseCaseHandler<RemoveProductByNameRequest, RemoveProductByNameResponse>
    {
    }
}