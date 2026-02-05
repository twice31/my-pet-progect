using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Contracts
{
    public interface ITransactionFactory
    {
        Task<ITransaction> BeginTransactionAsync(CancellationToken ct = default);
    }
}