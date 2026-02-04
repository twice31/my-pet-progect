using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Contracts
{
    public interface IDbTransactionManager
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}