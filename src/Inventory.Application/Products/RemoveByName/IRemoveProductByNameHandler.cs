using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.RemoveByName
{
    /// <summary>
    /// Manages the use case of the deletion of a product
    /// </summary>
    public interface IRemoveProductByNameHandler : IUseCaseHandler<RemoveProductByNameRequest, RemoveProductByNameResponse>
    {
    }
}