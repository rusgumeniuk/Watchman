using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Watchman.BusinessLogic.Models;

namespace Identity.API.Services
{
    public class Repository<TEnity, TKey> : IRepository<TEnity, TKey>
        where TEnity : class, IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public void Add(TEnity entity)
        {
            Context.Set<TEnity>().Add(entity);
        }

        public TEnity Get(TKey id)
        {
            return Context.Set<TEnity>().Find(id);
        }

        public IEnumerable<TEnity> GetAll()
        {
            return Context.Set<TEnity>().ToList();
        }

        public IEnumerable<TEnity> GetByCondition(Expression<Func<TEnity, bool>> expression)
        {
            return Context.Set<TEnity>().Where(expression);
        }

        public void Remove(TEnity entity)
        {
            Context.Set<TEnity>().Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
