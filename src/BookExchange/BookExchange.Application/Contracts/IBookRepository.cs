using Domain.Book;
using Domain.Book.VO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Contracts
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(BookId id, CancellationToken cancellationToken = default);
        Task<Book?> GetByIdWithLockAsync(BookId id, CancellationToken cancellationToken = default);
        Task AddAsync(Book book, CancellationToken cancellationToken = default);
        void Delete(Book book);
        Task<bool> ExistsAsync(BookId id, CancellationToken cancellationToken = default);
        Task<List<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}