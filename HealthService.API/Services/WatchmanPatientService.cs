using HealthService.API.Models.Repositories;
using HealthService.API.Models.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Services
{
    public class WatchmanPatientService : IWatchmanPatientService<Guid>
    {
        private readonly IWatchmanPatientUnitOfWork db;
        private readonly IHealthAnalyzer analyzer;
        public WatchmanPatientService(IWatchmanPatientUnitOfWork unitOfWork, IHealthAnalyzer analyzer)
        {
            this.db = unitOfWork;
            this.analyzer = analyzer;
        }

        public HealthMeasurement<Guid, Guid> GetLastHealthMeasurement(Guid patientId)
        {
            var patient = db.PatientRepository.Retrieve(patientId);
            if (patient != null)
            {
                return db.PatientRepository.GetLastHealthMeasurement(patientId);
            }
            return null;
        }
        public IEnumerable<HealthMeasurement<Guid, Guid>> GetLastHealthMeasurements(Guid patientId, int count)
        {
            var patient = db.PatientRepository.Retrieve(patientId);
            if (patient != null)
            {
                return db.PatientRepository.GetLastHealthMeasurements(patientId, count);
            }
            return null;
        }
        public void AddHealthMeasurement(Guid patientId, HealthMeasurement<Guid, Guid> healthMeasurement)
        {
            var patient = db.PatientRepository.Retrieve(patientId);
            if (patient != null && healthMeasurement != null)
            {
                db.PatientRepository.AddHealthMeasurement(patientId, healthMeasurement);
                db.Save();
            }
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

        public void AddIgnorableSignToPatient(Guid patientId, Sign<Guid> sign)
        {
            var patient = db.PatientRepository.Retrieve(patientId);            
            if (patient != null)
            {
                db.PatientRepository.AddIgnorableSign(patientId, sign);
                db.Save();
            }
        }

        public IAnalysisResult AnalyzeLastMeasurement(Guid patientId)
        {
            var patient = db.PatientRepository.Retrieve(patientId);
            if(patient != null)
            {
                var patientWithProps = db.PatientRepository.RetrieveWithAllProperties(patientId);
                analyzer.AnalyzeLast(patientWithProps);
                return analyzer.AnalysisResult as IAnalysisResult;
            }
            return null;
        }

        public Patient<Guid> GetPatientByUserId(Guid userId)
        {
            return db.PatientRepository.RetrieveByUserId(userId);
        }

        public WatchmanProfile GetWatchmanByUserId(Guid userId)
        {
            return db.WatchmanRepository.RetrieveByUserId(userId);
        }

        public Patient<Guid> GetPatientWithPropertiesByUserId(Guid userId)
        {
            return db.PatientRepository.RetrieveWithPropertiesByUserId(userId);
        }

        public WatchmanProfile GetWatchmanWithPropertiesByUserId(Guid userId)
        {
            var res = db.WatchmanRepository.RetrieveWithPropertiesByUserId(userId);
            return res;
        }
    }
}
