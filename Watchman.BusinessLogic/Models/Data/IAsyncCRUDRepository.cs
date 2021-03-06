﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IAsyncCRUDRepository<TEntity, TKey> : IDisposable
        where TEntity : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Remove(TEntity entity);

        Task<TEntity> RetrieveAsync(TKey id);
        Task<TEntity> RetrieveWithAllPropertiesAsync(TKey id);
        Task<IEnumerable<TEntity>> RetrieveAllAsync();

        Task SaveChangesAsync();
        Task DisposeAsync();
    }
}
