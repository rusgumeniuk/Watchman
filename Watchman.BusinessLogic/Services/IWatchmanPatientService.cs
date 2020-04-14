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
        Task<IAnalysisResult> AnalyzeLastMeasurementAsync(TKey patientId);

        Task<HealthMeasurement<TKey, TKey>> GetLastHealthMeasurementAsync(TKey patientId);
        Task<IEnumerable<HealthMeasurement<TKey, TKey>>> GetLastHealthMeasurementsAsync(TKey patientId, int count);
        Task AddHealthMeasurementAsync(TKey patientId, HealthMeasurement<TKey, TKey> healthMeasurement);

        Task AddIgnorableSignToPatientAsync(TKey patientId, Sign<TKey> sign);

        Task AddPatientToWatchmanAsync(TKey watchmanId, TKey patientId);

        Task RemovePatientFromWatchmanAsync(TKey watchmanId, TKey patientId);

        Task CreatePatient(Patient<TKey> patient, string token = null);
        Task CreateWatchman(WatchmanProfile<TKey> watchman);

        void RemoveAllWatchmenFromPatient(TKey patientId);
        void RemoveAllPatientFromWatchman(TKey watchmanId);
    }
}
