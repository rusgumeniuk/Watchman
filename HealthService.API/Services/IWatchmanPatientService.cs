using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Services
{
    public interface IWatchmanPatientService<TKey>
        where TKey : IEquatable<TKey>
    {
        HealthMeasurement<TKey, TKey> GetLastHealthMeasurement(TKey patientId);
        IEnumerable<HealthMeasurement<TKey, TKey>> GetLastHealthMeasurements(TKey patientId, int count);
        void AddHealthMeasurement(TKey patientId, HealthMeasurement<TKey, TKey> healthMeasurement);

        void AddIgnorableSignToPatient(TKey patientId, Sign<TKey> sign);

        void AddPatientToWatchman(TKey watchmanId, TKey patientId);

        void RemovePatientFromWatchman(TKey watchmanId, TKey patientId);

        bool ExistPatient(TKey userId);
        bool ExistWatchman(TKey userId);

        void AddPatientToUser(TKey userId, Patient<TKey> patient = null);
        void AddWatchmanToUser(TKey userId, WatchmanProfile watchman = null);

        void CreateIfNotExistPatient(TKey userId);
        void CreateIfNotExistWatchman(TKey userId);

        void RemovePatientFromUser(TKey userId);
        void RemoveWatchmanFromUser(TKey userId);

        void RemoveAllWatchmenFromPatient(TKey patientId);
        void RemoveAllPatientFromWatchman(TKey watchmanId);
    }
}
