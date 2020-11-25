using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Domain.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}