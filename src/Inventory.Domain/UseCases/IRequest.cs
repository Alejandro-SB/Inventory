namespace Inventory.Domain.UseCases
{
    /// <summary>
    /// Represents a basic request
    /// </summary>
    /// <typeparam name="TUseCaseResponse"></typeparam>
    public interface IRequest<out TUseCaseResponse> { }
}