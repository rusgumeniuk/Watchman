using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Watchman.BusinessLogic.Models;
using Watchman.BusinessLogic.Models.Data;

namespace Identity.API.Repositories
{
    public class Repository<TEntity, TKey> : ICRUDRepository<TEntity, TKey>
        where TEntity : class, IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public TEntity Update(TEntity oldEntity, TKey id = default)
        {
            Context.Entry(oldEntity).State = EntityState.Modified;
            Context.SaveChanges();
            return Context.Set<TEntity>().Find(oldEntity.Id);
        }
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public TEntity Retrieve(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> RetrieveAll(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
