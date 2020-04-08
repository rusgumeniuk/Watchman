using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models;
using Watchman.BusinessLogic.Models.Data;

namespace HealthService.API.Models.Repositories
{
    public abstract class Repository<TEntity, TKey> : IAsyncCRUDRepository<TEntity, TKey>
        where TEntity : class, IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        public async Task<TEntity> UpdateAsync(TEntity oldEntity)
        {
            Context.Entry(oldEntity).State = EntityState.Modified;
            Context.SaveChanges();
            return await Context.Set<TEntity>().FindAsync(oldEntity.Id);
        }
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> RetrieveAsync(TKey id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> RetrieveAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> RetrieveAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
        public async Task DisposeAsync()
        {
            await Context.DisposeAsync();
        }

        public abstract Task<TEntity> RetrieveWithAllPropertiesAsync(TKey id);
    }
}
