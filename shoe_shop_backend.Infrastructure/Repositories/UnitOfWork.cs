using Microsoft.EntityFrameworkCore;
using shoe_shop_backend.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        private bool _disposed;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public ICommonRepository<T> Repository<T>() where T : class
        {
            return (ICommonRepository<T>)_repositories.GetOrAdd(
                typeof(T),
                _ => new CommonRepository<T>(_context));
        }

        // ---------- SAVE CHANGES ----------
        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        // ---------- TRANSACTION WRAPPER: void, sync ----------
        public void ExecuteTransaction(Action action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                action();
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ---------- TRANSACTION WRAPPER: void, async ----------
        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // ---------- TRANSACTION WRAPPER: có return value, sync ----------
        public T ExecuteTransaction<T>(Func<T> action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var result = action();
                _context.SaveChanges();
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ---------- TRANSACTION WRAPPER: có return value, async ----------
        public async Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await action();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // ---------- DISPOSE ----------
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _context.Dispose();
            _disposed = true;
        }
    }
}
