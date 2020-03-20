using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IWatchmanRepository<TWatchman, TKey> : IAsyncCRUDRepository<TWatchman, TKey>
        where TWatchman : WatchmanProfile<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<Patient<TKey>> GetPatients(TWatchman watchman);

        bool ExistWatchmanProfile(TKey userId);
        bool ExistWatchmanProfile<TUser>(TUser user)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;

        void AddWatchmanToUser(TKey userId, TWatchman watchman = null);
        void AddWatchmanToUser<TUser>(TUser user, TWatchman watchman = null)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;

        void RemoveWatchmanFromUser(TKey userId);
        void RemoveWatchmanFromUser<TUser>(TUser user)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;
    }
}
