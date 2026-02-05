using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Contracts
{
    public interface ITransaction : IAsyncDisposable
    {
        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
    }
}