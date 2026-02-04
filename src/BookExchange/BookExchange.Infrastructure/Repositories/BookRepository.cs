using BookExchange.Application.Contracts;
using Domain.Book;
using Domain.Book.VO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
        {
            await _dbContext.Books.AddAsync(book, cancellationToken);
        }

        public void Delete(Book book)
        {
            _dbContext.Books.Remove(book);
        }

        public async Task<Book?> GetByIdAsync(BookId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Books
                .SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<Book?> GetByIdWithLockAsync(BookId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Books
                .FromSqlRaw("SELECT * FROM \"Books\" WHERE \"Id\" = {0} FOR UPDATE", id.Value)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(BookId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Books.AnyAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Books.ToListAsync(cancellationToken);
        }
    }
}