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

        public Task AddAsync(Book book)
        {
            _dbContext.Books.Add(book);
            return Task.CompletedTask;
        }

        public void Delete(Book book)
        {
            _dbContext.Books.Remove(book);
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
        {
            return await _dbContext.Books.AsNoTracking().ToListAsync();
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

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Update(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
        }
    }
}