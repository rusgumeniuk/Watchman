using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models;
using Watchman.BusinessLogic.Models.Data;

namespace Watchman.API.Common.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TKey> : IAsyncCRUDRepository<TEntity, TKey>
        where TEntity : class, IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext _context;

        protected Repository(DbContext context)
        {
            this._context = context;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity oldEntity)
        {
            _context.Entry(oldEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Set<TEntity>().FindAsync(oldEntity.Id);
        }
        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> RetrieveAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<TEntity>> RetrieveAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> RetrieveAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public abstract Task<TEntity> RetrieveWithAllPropertiesAsync(TKey id);
    }
}
