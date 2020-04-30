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
        Task AddWatchmanToUserAsync(TKey userId, TKey watchmanId, string token = null);

        Task RemovePatientFromUser(TKey userId, string token = null);
        Task RemoveWatchmanFromUser(TKey userId, string token = null);

        Task<Patient<TKey>> GetPatientByUserIdAsync(TKey usedId);
        Task<WatchmanProfile> GetWatchmanByUserIdAsync(TKey userId);        
    }
}
