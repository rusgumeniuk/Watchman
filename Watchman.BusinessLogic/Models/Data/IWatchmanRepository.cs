using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IWatchmanRepository<TWatchman, TKey> : IAsyncCRUDRepository<TWatchman, TKey>
        where TWatchman : WatchmanProfile<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<Patient<TKey>>> GetPatientsAsync(TWatchman watchman);

        Task<TWatchman> RetrieveByUserIdAsync(Guid userId);
        Task<TWatchman> RetrieveWithPropertiesByUserIdAsync(Guid userId);

        Task<bool> ExistWatchmanProfileAsync(TKey userId);
        Task<bool> ExistWatchmanProfileAsync<TUser>(TUser user)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;

        Task AddWatchmanToUserAsync(TKey userId, TWatchman watchman = null);
        Task AddWatchmanToUserAsync<TUser>(TUser user, TWatchman watchman = null)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;

        void RemoveWatchmanFromUser(TKey userId);
        void RemoveWatchmanFromUser<TUser>(TUser user)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;
    }
}
