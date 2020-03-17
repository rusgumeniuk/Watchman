using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IAsyncCRUDRepository<TEntity, TKey> : ICRUDRepository<TEntity, TKey>
        where TEntity : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> RetrieveAsync(TKey id);
        Task<IEnumerable<TEntity>> RetrieveAll();

        Task SaveChangesAsync();
        Task DisposeAsync();
    }
}
