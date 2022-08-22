using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YB.Todo.Contracts;

namespace YB.Todo.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            await _dbContext.AddAsync(entity);
            //await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Update(entity);
            await Task.Delay(0);
            //await _dbContext.SaveChangesAsync();
        }

        public async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Remove(entity);
            await Task.Delay(0);
            //await _dbContext.SaveChangesAsync();
        }

        protected IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        protected async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        protected virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        protected virtual ValueTask<TEntity> FindAsync(params object[] keyValues) => _dbSet.FindAsync(keyValues);

        protected virtual ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);
    }
}
