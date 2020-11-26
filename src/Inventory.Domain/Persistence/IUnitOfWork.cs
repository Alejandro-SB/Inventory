using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Domain.Persistence
{
    /// <summary>
    /// Represents a unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all the changes made
        /// </summary>
        /// <param name="cancellationToken">The token to cancel the operation</param>
        /// <returns>The number of entities modified</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}