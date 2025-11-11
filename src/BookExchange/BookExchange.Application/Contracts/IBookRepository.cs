using Domain.Book;
using Domain.Book.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookExchange.Application.Contracts
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(BookId id);
        Task<IReadOnlyList<Book>> GetAllAsync();
        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);

       
        Task<int> SaveChangesAsync();
        Task<bool> ExistsAsync(BookId id);
    }
}