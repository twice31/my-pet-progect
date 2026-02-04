using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.User;
using Domain.Book;
using Domain.ExchangeRequest;
using BookExchange.Application.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookExchange.Infrastructure.Data
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly string _connectionString;
        private IDbContextTransaction? _currentTransaction;

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ExchangeRequest> ExchangeRequests { get; set; }

        public ApplicationDbContext(IOptions<PostgresConnectionOptions> options)
        {
            _connectionString = options.Value.AsConnectionString();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;
            _currentTransaction = await Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null) return;
            await _currentTransaction.CommitAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null) return;
            await _currentTransaction.RollbackAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}