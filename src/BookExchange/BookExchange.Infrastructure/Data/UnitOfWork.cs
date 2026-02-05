using BookExchange.Application.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Infrastructure.Data
{
    public class EfTransaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;
        public EfTransaction(IDbContextTransaction transaction) => _transaction = transaction;

        public Task CommitAsync(CancellationToken ct) => _transaction.CommitAsync(ct);
        public Task RollbackAsync(CancellationToken ct) => _transaction.RollbackAsync(ct);

        public async ValueTask DisposeAsync() => await _transaction.DisposeAsync();
    }

    public class EfTransactionFactory : ITransactionFactory
    {
        private readonly ApplicationDbContext _context;
        public EfTransactionFactory(ApplicationDbContext context) => _context = context;

        public async Task<ITransaction> BeginTransactionAsync(CancellationToken ct)
        {
            var tx = await _context.Database.BeginTransactionAsync(ct);
            return new EfTransaction(tx);
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) => _context = context;

        public Task<int> SaveChangesAsync(CancellationToken ct) => _context.SaveChangesAsync(ct);
    }
}