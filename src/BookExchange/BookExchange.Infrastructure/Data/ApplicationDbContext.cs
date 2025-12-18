using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.User;
using Domain.Book;
using Domain.ExchangeRequest;
using BookExchange.Application.Contracts;

namespace BookExchange.Infrastructure.Data
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly string _connectionString;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}