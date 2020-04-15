using HealthService.API.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.API.Common.Infrastructure.Repositories;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Infrastructure.Repositories
{
    public class PatientRepository : Repository<PatientProfile, Guid>, IPatientRepository<PatientProfile, Guid>
    {
        public HealthDbContext HealthContext => Context as HealthDbContext;
        public PatientRepository(HealthDbContext context) : base(context) { }

        public async Task<HealthMeasurement<Guid, Guid>> GetLastHealthMeasurementAsync(Guid patientId)
        {
            var patient = await HealthContext
                .Patients
                .Include(pat => pat.HealthMeasurements)
                    .ThenInclude(hm => hm.Signs)
                .FirstAsync(pat => pat.Id.Equals(patientId));
            return patient.HealthMeasurements.ElementAt(GetIndexOfItemWithNewDate(patient.HealthMeasurements));
        }
        public async Task<IEnumerable<HealthMeasurement<Guid, Guid>>> GetLastHealthMeasurementsAsync(Guid patientId, int count)
        {
            var patient = await HealthContext
                .Patients
                .Include(pat => pat.HealthMeasurements)
                    .ThenInclude(hm => hm.Signs)
                .FirstAsync(pat => pat.Id.Equals(patientId));
            return patient.HealthMeasurements.TakeLast(count);
        }
        public async Task AddHealthMeasurementAsync(Guid patientId, HealthMeasurement<Guid, Guid> healthMeasurement)
        {
            var patient = await HealthContext
                .Patients
                .Include(pat => pat.HealthMeasurements)
                .FirstAsync(pat => pat.Id.Equals(patientId));
            patient.HealthMeasurements.Add(healthMeasurement);
        }

        public async Task<IEnumerable<WatchmanProfile<Guid>>> GetWatchmenOfPatientAsync(PatientProfile patient)
        {
            return await HealthContext
                .WatchmanPatients
                .Where(pair => pair.PatientId.Equals(patient.Id))
                .Select(pair => pair.Watchman)
                .ToListAsync();
        }


        public async override Task<PatientProfile> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await HealthContext
                .Patients
                .Include(pat => pat.IgnorableSignPair)
                .Include(pat => pat.HealthMeasurements)
                    .ThenInclude(hm => hm.Signs)
                .Include(pat => pat.WatchmanPatients)
                .FirstAsync(pat => pat.Id.Equals(id));
        }


        public async Task AddIgnorableSignAsync(Guid patientId, Sign<Guid> sign)
        {
            var patient = await HealthContext
                .Patients
                .Include(pat => pat.IgnorableSignPair)
                .FirstAsync(pat => pat.Id.Equals(patientId));
            patient
                .IgnorableSignPair
                .Add(new PatientSign<Guid, ushort>() { PatientId = patientId, SignType = sign.GetType().ToString() });
        }

        private int GetIndexOfItemWithNewDate(IEnumerable<HealthMeasurement<Guid, Guid>> list)
        {
            if (list.Count() < 1)
                return -1;
            int index = 0;
            for (int i = 0; i < list.Count() - 1; ++i)
            {
                if (list.ElementAt(i).MeasurementTime <= list.ElementAt(i + 1).MeasurementTime)
                    index = i + 1;
            }
            return index;
        }
    }
}
