using Microsoft.EntityFrameworkCore;
using Domain.User;
using Domain.Book;
using Domain.ExchangeRequest;
using System.Reflection;
using BookExchange.Application.Contracts;
using System.Threading.Tasks; 

namespace BookExchange.Infrastructure.Data
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ExchangeRequest> ExchangeRequests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public new Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}