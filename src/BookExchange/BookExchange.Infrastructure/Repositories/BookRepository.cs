using BookExchange.Application.Contracts;
using Domain.Book;
using Domain.Book.VO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using BookExchange.Infrastructure.Data;

namespace BookExchange.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
        }

        public void Delete(Book book)
        {
            _dbContext.Books.Remove(book);
        }


        public async Task<Book?> GetByIdAsync(BookId id)
        {
            return await _dbContext.Books
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public Task<bool> ExistsAsync(BookId id)
        {
            return _dbContext.Books.AnyAsync(b => b.Id == id);
        }

    }
}