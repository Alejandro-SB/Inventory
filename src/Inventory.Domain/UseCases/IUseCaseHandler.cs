using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Domain.UseCases
{
    /// <summary>
    /// Defines a use case that handles a specific request with a response
    /// </summary>
    /// <typeparam name="TUseCaseRequest">The type of the request</typeparam>
    /// <typeparam name="TUseCaseResponse">The type of the response</typeparam>
    public interface IUseCaseHandler<in TUseCaseRequest, TUseCaseResponse>
        where TUseCaseRequest : IRequest<TUseCaseResponse>
    {
        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request">The request to fulfill</param>
        /// <param name="cancellationToken">The token to cancel the operation</param>
        /// <returns>The response of the operation</returns>
        Task<TUseCaseResponse> Handle(TUseCaseRequest request, CancellationToken cancellationToken = default);
    }
}