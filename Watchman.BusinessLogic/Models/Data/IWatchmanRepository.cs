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
    }
}
