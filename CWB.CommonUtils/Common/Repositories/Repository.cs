using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CWB.CommonUtils.Common.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            this.Context = context;
            _dbSet = Context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> GetRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public ValueTask<TEntity> GetByIdAsync(long id)
        {
            return _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> UpdateAsync(long id, TEntity entity)
        {
            var currentEntity = await _dbSet.FindAsync(id);
            if (currentEntity != null)
            {
                Context.Entry(currentEntity).CurrentValues.SetValues(entity);
                return entity;
            }
            return null;
        }


    }
}
