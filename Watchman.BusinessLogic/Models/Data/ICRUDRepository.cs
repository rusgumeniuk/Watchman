using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Watchman.BusinessLogic.Models.Data
{
    public interface ICRUDRepository<TEntity, TKey> : IDisposable
        where TEntity : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Create(TEntity entity);
        TEntity Update(TEntity oldEntity, TKey id = default);
        void Remove(TEntity entity);

        TEntity Retrieve(TKey id);
        TEntity RetrieveWithAllProperties(TKey id);
        IEnumerable<TEntity> RetrieveAll(Expression<Func<TEntity, bool>> expression);

        void SaveChanges();
    }
}
