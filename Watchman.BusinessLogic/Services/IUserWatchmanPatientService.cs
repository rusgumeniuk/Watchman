using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Services
{
    public interface IUserWatchmanPatientService<TKey>
         where TKey : IEquatable<TKey>
    {
        Task<bool> ExistPatientAsync(TKey userId);
        Task<bool> ExistWatchmanAsync(TKey userId);

        Task AddPatientToUserAsync(TKey userId, TKey patientId, string token = null);
        Task AddWatchmanToUserAsync(TKey userId, TKey watchmanId);

        Task<TKey> CreateIfNotExistPatientAsync(TKey userId);
        Task<TKey> CreateIfNotExistWatchmanAsync(TKey userId);

        void RemovePatientFromUser(TKey userId);
        void RemoveWatchmanFromUser(TKey userId);

        Task<Patient<TKey>> GetPatientByUserIdAsync(TKey usedId);
        Task<WatchmanProfile> GetWatchmanByUserIdAsync(TKey userId);

        Task<Patient<TKey>> GetPatientWithPropertiesByUserIdAsync(TKey usedId);
        Task<WatchmanProfile> GetWatchmanWithPropertiesByUserIdAsync(TKey userId);
    }
}
