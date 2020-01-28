
using System;
using System.Linq;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IQueryableStore<TClass, TKey> : IStore<TClass, TKey>
        where TClass : class, IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IQueryable<TClass> Set { get; }
    }
}
