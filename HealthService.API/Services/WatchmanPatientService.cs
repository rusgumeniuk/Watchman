using HealthService.API.Models.Infrastructure.Repositories;
using HealthService.API.Models.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace HealthService.API.Services
{
    public class WatchmanPatientService : IWatchmanPatientService<Guid>
    {
        private readonly IWatchmanPatientUnitOfWork _db;
        private readonly IHealthAnalyzer _analyzer;
        public WatchmanPatientService(IWatchmanPatientUnitOfWork unitOfWork, IHealthAnalyzer analyzer)
        {
            this._db = unitOfWork;
            this._analyzer = analyzer;
        }

        public async Task AddIgnorableSignToPatientAsync(Guid patientId, Sign<Guid> sign, string token = null)
        {
            var patient = _db.PatientRepository.RetrieveAsync(patientId);
            if (patient != null)
            {
                await _db.PatientRepository.AddIgnorableSignAsync(patientId, sign);
                await _db.SaveAsync();
            }
        }

        public async Task<IAnalysisResult> AnalyzeLastMeasurementAsync(Guid patientId, string token = null)
        {
            var patient = await _db.PatientRepository.RetrieveAsync(patientId);
            if (patient != null)
            {
                var patientWithProps = await _db.PatientRepository.RetrieveWithAllPropertiesAsync(patientId);
                _analyzer.AnalyzeLast(patientWithProps);
                return _analyzer.AnalysisResult as IAnalysisResult;
            }
            return null;
        }

        public async Task<HealthMeasurement<Guid, Guid>> GetLastHealthMeasurementAsync(Guid patientId, string token = null)
        {
            var patient = await _db.PatientRepository.RetrieveAsync(patientId);
            if (patient != null)
            {
                return await _db.PatientRepository.GetLastHealthMeasurementAsync(patientId);
            }
            return null;
        }
        public async Task<IEnumerable<HealthMeasurement<Guid, Guid>>> GetLastHealthMeasurementsAsync(Guid patientId, int count, string token = null)
        {
            var patient = await _db.PatientRepository.RetrieveAsync(patientId);
            if (patient != null)
            {
                return await _db.PatientRepository.GetLastHealthMeasurementsAsync(patientId, count);
            }
            return null;
        }
        public async Task AddHealthMeasurementAsync(Guid patientId, HealthMeasurement<Guid, Guid> healthMeasurement, string token = null)
        {
            var patient = _db.PatientRepository.RetrieveAsync(patientId);
            if (patient != null && healthMeasurement != null)
            {
                await _db.PatientRepository.AddHealthMeasurementAsync(patientId, healthMeasurement);
                await _db.SaveAsync();
            }
        }

        public async Task AddPatientToWatchmanAsync(Guid watchmanId, Guid patientId, string token = null)
        {
            var watchman = _db.WatchmanRepository.RetrieveAsync(watchmanId);
            var patient = _db.PatientRepository.RetrieveAsync(patientId);

            if (watchman != null && patient != null)
            {
                await _db.AddWatchmanToPatientAsync(watchmanId, patientId);
                await _db.SaveAsync();
            }
        }
        public async Task RemovePatientFromWatchmanAsync(Guid watchmanId, Guid patientId, string token = null)
        {
            var watchman = await _db.WatchmanRepository.RetrieveAsync(watchmanId);
            if (watchman != null)
            {
                var patients = new List<Patient<Guid>>(await _db.WatchmanRepository.GetPatientsAsync(watchman));
                if (patients.FirstOrDefault(patient => patient.Id.Equals(patientId)) != null)
                {
                    _db.RemoveWatchmanFromPatient(watchmanId, patientId);
                    await _db.SaveAsync();
                }
            }
        }

        public void RemoveAllWatchmenFromPatient(Guid patientId, string token = null)
        {
            var patient = _db.PatientRepository.RetrieveAsync(patientId);
            if (patient != null)
            {
                _db.RemoveAllWatchmen(patientId);
                _db.SaveAsync();
            }
        }
        public void RemoveAllPatientFromWatchman(Guid watchmanId, string token = null)
        {
            var watchman = _db.WatchmanRepository.RetrieveAsync(watchmanId);
            if (watchman != null)
            {
                _db.RemoveAllPatients(watchmanId);
                _db.SaveAsync();
            }
        }

        public async Task CreatePatientAsync(Patient<Guid> patient, string token = null)
        {
            await _db.PatientRepository.CreateAsync(patient as PatientProfile);
            await _db.SaveAsync();
        }
        public async Task CreateWatchmanAsync(WatchmanProfile<Guid> watchman, string token = null)
        {
            await _db.WatchmanRepository.CreateAsync(watchman as WatchmanProfileHealth);
            await _db.SaveAsync();
        }

        public async Task<Patient<Guid>> GetPatientAsync(Guid id, string token = null)
        {
            return await _db.PatientRepository.RetrieveAsync(id);
        }
        public async Task<WatchmanProfile<Guid>> GetWatchmanAsync(Guid id, string token = null)
        {
            return await _db.WatchmanRepository.RetrieveAsync(id);
        }
    }
}
