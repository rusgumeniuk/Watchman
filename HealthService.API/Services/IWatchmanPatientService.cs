using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Services
{
    public interface IWatchmanPatientService<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IAnalysisResult> AnalyzeLastMeasurementAsync(TKey patientId);

        Task<HealthMeasurement<TKey, TKey>> GetLastHealthMeasurementAsync(TKey patientId);
        Task<IEnumerable<HealthMeasurement<TKey, TKey>>> GetLastHealthMeasurementsAsync(TKey patientId, int count);
        Task AddHealthMeasurementAsync(TKey patientId, HealthMeasurement<TKey, TKey> healthMeasurement);

        Task AddIgnorableSignToPatientAsync(TKey patientId, Sign<TKey> sign);

        Task AddPatientToWatchmanAsync(TKey watchmanId, TKey patientId);

        Task RemovePatientFromWatchmanAsync(TKey watchmanId, TKey patientId);

        Task<bool> ExistPatientAsync(TKey userId);
        Task<bool> ExistWatchmanAsync(TKey userId);

        Task AddPatientToUserAsync(TKey userId, Patient<TKey> patient = null);
        Task AddWatchmanToUserAsync(TKey userId, WatchmanProfile watchman = null);

        Task CreateIfNotExistPatientAsync(TKey userId);
        Task CreateIfNotExistWatchmanAsync(TKey userId);

        void RemovePatientFromUser(TKey userId);
        void RemoveWatchmanFromUser(TKey userId);

        void RemoveAllWatchmenFromPatient(TKey patientId);
        void RemoveAllPatientFromWatchman(TKey watchmanId);

        Task<Patient<TKey>> GetPatientByUserIdAsync(TKey usedId);
        Task<WatchmanProfile> GetWatchmanByUserIdAsync(TKey userId);

        Task<Patient<TKey>> GetPatientWithPropertiesByUserIdAsync(TKey usedId);
        Task<WatchmanProfile> GetWatchmanWithPropertiesByUserIdAsync(TKey userId);
    }
}
