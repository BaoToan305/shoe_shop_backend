using Microsoft.EntityFrameworkCore;
using shoe_shop_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Infrastructure.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public CommonRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ---------- SYNC ----------
        public T? GetById(string id) => _dbSet.Find(id);

        public IEnumerable<T> GetAll() => _dbSet.AsNoTracking().ToList();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
            => _dbSet.AsNoTracking().Where(predicate).ToList();

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
            => _dbSet.AsNoTracking().FirstOrDefault(predicate);

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void DeleteById(string id)
        {
            var entity = GetById(id);
            if (entity != null) Delete(entity);
        }

        public bool Exists(Expression<Func<T, bool>> predicate) => _dbSet.Any(predicate);

        public int Count(Expression<Func<T, bool>>? predicate = null)
            => predicate == null ? _dbSet.Count() : _dbSet.Count(predicate);

        // ---------- ASYNC ----------
        public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().SingleOrDefaultAsync(predicate, cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbSet.AddAsync(entity, cancellationToken);

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null) await DeleteAsync(entity, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(predicate, cancellationToken);

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
            => predicate == null
                ? await _dbSet.CountAsync(cancellationToken)
                : await _dbSet.CountAsync(predicate, cancellationToken);

        // ---------- BULK ----------
        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _dbSet.AddRangeAsync(entities, cancellationToken);

        public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
