using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Watchman.BusinessLogic.Models;

namespace Identity.API.Services
{
    public interface IRepository<TEnity, TKey>
        where TEnity : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEnity Get(TKey id);
        IEnumerable<TEnity> GetAll();
        IEnumerable<TEnity> GetByCondition(Expression<Func<TEnity, bool>> expression);

        void Add(TEnity entity);

        void Remove(TEnity entity);

        void SaveChanges();
    }
}
