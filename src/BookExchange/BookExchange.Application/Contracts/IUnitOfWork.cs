using System.Threading.Tasks;

namespace BookExchange.Application.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}