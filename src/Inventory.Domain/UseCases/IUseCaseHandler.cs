using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Domain.UseCases
{
    public interface IUseCaseHandler<in TUseCaseRequest, TUseCaseResponse>
        where TUseCaseRequest : IRequest<TUseCaseResponse>
    {
        Task<TUseCaseResponse> Handle(TUseCaseRequest request, CancellationToken cancellationToken = default);
    }
}