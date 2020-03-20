using System;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Services
{
    public interface IWatchmanPatientService<TKey>
        where TKey : IEquatable<TKey>
    {
        void AddPatientToWatchman(Guid watchmanId, Guid patientId);

        void RemovePatientFromWatchman(Guid watchmanId, Guid patientId);

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
