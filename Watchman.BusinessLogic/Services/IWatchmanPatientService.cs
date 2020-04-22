using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Services
{
    public interface IWatchmanPatientService<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IAnalysisResult> AnalyzeLastMeasurementAsync(TKey patientId, string token = null);

        Task<HealthMeasurement<TKey, TKey>> GetLastHealthMeasurementAsync(TKey patientId, string token = null);
        Task<IEnumerable<HealthMeasurement<TKey, TKey>>> GetLastHealthMeasurementsAsync(TKey patientId, int count, string token = null);
        Task AddHealthMeasurementAsync(TKey patientId, HealthMeasurement<TKey, TKey> healthMeasurement, string token = null);

        Task AddIgnorableSignToPatientAsync(TKey patientId, Sign<TKey> sign, string token = null);

        Task AddPatientToWatchmanAsync(TKey watchmanId, TKey patientId, string token = null);
        Task<bool> IsControlPatient(TKey watchmanId, TKey patientId, string token = null);

        Task RemovePatientFromWatchmanAsync(TKey watchmanId, TKey patientId, string token = null);

        Task CreatePatientAsync(Patient<TKey> patient, string token = null);
        Task CreateWatchmanAsync(WatchmanProfile<TKey> watchman, string token = null);

        Task<Patient<TKey>> GetPatientAsync(TKey id, string token = null);
        Task<WatchmanProfile<TKey>> GetWatchmanAsync(TKey id, string token = null);

        void RemoveAllWatchmenFromPatient(TKey patientId, string token = null);
        void RemoveAllPatientFromWatchman(TKey watchmanId, string token = null);
    }
}
