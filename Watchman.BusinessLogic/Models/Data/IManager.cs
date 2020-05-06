
using System;
using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IManager<TClass, TKey> : IDisposable
        where TClass : class, IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        ICRUDRepository<TClass, TKey> Repository { get; }

        Task<OperationResult> CreateAsync(TClass obj);
        Task<OperationResult> UpdateAsync(TClass obj);
        Task<OperationResult> DeleteAsync(TClass obj);
        Task<OperationResult> FindByIdAsync(TKey id);
        Task<TKey> GetId(TClass obj);
        void Dispose(bool disposing);
        void ThrowIfDisposed();
    }
}
