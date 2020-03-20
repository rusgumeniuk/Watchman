using HealthService.API.Models.Repositories;
using HealthService.API.Models.Users;

using System;
using System.Collections.Generic;
using System.Linq;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Services
{
    public class WatchmanPatientService : IWatchmanPatientService<Guid>
    {
        private readonly IWatchmanPatientUnitOfWork db;
        public WatchmanPatientService(IWatchmanPatientUnitOfWork unitOfWork)
        {
            this.db = unitOfWork;
        }

        public void CreateIfNotExistPatient(Guid userId)
        {
            if (!ExistPatient(userId))
                AddPatientToUser(userId);
        }
        public void CreateIfNotExistWatchman(Guid userId)
        {
            if (!ExistWatchman(userId))
                AddWatchmanToUser(userId);
        }

        public bool ExistPatient(Guid userId)
        {
            return db.PatientRepository.ExistPatientProfile(userId);
        }
        public bool ExistWatchman(Guid userId)
        {
            return db.WatchmanRepository.ExistWatchmanProfile(userId);
        }

        public void AddPatientToUser(Guid userId, Patient<Guid> patient = null)
        {
            db.PatientRepository.AddPatientToUser(userId, patient as PatientProfile);
            db.Save();
        }
        public void AddWatchmanToUser(Guid userId, WatchmanProfile watchman = null)
        {
            db.WatchmanRepository.AddWatchmanToUser(userId, watchman as WatchmanProfileHealth);
            db.Save();
        }

        public void RemovePatientFromUser(Guid userId)
        {
            db.PatientRepository.RemovePatientFromUser(userId);
            db.Save();
        }
        public void RemoveWatchmanFromUser(Guid userId)
        {
            db.WatchmanRepository.RemoveWatchmanFromUser(userId);
            db.Save();
        }

        public void AddPatientToWatchman(Guid watchmanId, Guid patientId)
        {
            var watchman = db.WatchmanRepository.Retrieve(watchmanId);
            var patient = db.PatientRepository.Retrieve(patientId);

            if (watchman != null && patient != null)
            {
                db.AddWatchmanToPatient(watchmanId, patientId);
                db.Save();
            }
        }
        public void RemovePatientFromWatchman(Guid watchmanId, Guid patientId)
        {
            var watchman = db.WatchmanRepository.Retrieve(watchmanId);
            if (watchman != null)
            {
                var patients = new List<Patient<Guid>>(db.WatchmanRepository.GetPatients(watchman));
                if (patients.FirstOrDefault(patient => patient.Id.Equals(patientId)) != null)
                {
                    db.RemoveWatchmanFromPatient(watchmanId, patientId);
                    db.Save();
                }
            }
        }

        public void RemoveAllWatchmenFromPatient(Guid patientId)
        {
            var patient = db.PatientRepository.Retrieve(patientId);
            if (patient != null)
            {
                db.RemoveAllWatchmen(patientId);
                db.Save();
            }
        }
        public void RemoveAllPatientFromWatchman(Guid watchmanId)
        {
            var watchman = db.WatchmanRepository.Retrieve(watchmanId);
            if (watchman != null)
            {
                db.RemoveAllPatients(watchmanId);
                db.Save();
            }
        }
    }
}
