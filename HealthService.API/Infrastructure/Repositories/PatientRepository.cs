using HealthService.API.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public async Task AddPatientToUserAsync(Guid userId, PatientProfile patient = null)
        {
            var user = await HealthContext.Users.FindAsync(userId);
            user.Patient = patient ?? new PatientProfile();
        }
        public async Task AddPatientToUserAsync<TUser>(TUser user, PatientProfile patient = null) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            await AddPatientToUserAsync(user.Id, patient);
        }

        public async Task<bool> ExistPatientProfileAsync(Guid userId)
        {
            var user = await HealthContext
                .Users
                .Include(us => us.Patient)
                .FirstOrDefaultAsync(us => us.Id.Equals(userId));

            return user?.Patient != null;
        }
        public async Task<bool> ExistPatientProfileAsync<TUser>(TUser user) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            return await ExistPatientProfileAsync(user.Id);
        }

        public void RemovePatientFromUser(Guid userId)
        {
            var user = HealthContext
                .Users
                .Include(us => us.Patient)
                .FirstOrDefault(us => us.Id.Equals(userId));
            if (user != null && user.Patient != null)
                user.Patient = null;
        }
        public void RemovePatientFromUser<TUser>(TUser user) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            RemovePatientFromUser(user.Id);
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
                //.ThenInclude(pair => pair.Patient)
                .Include(pat => pat.HealthMeasurements)
                    .ThenInclude(hm => hm.Signs)
                .Include(pat => pat.WatchmanPatients)
                .FirstAsync(pat => pat.Id.Equals(id));
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

        public async Task<PatientProfile> RetrieveByUserIdAsync(Guid userId)
        {
            var res = await HealthContext
                .Users
                .Include(user => user.Patient)
                .FirstAsync(user => user.Id.Equals(userId));
            return res.Patient as PatientProfile;
        }

        public async Task<PatientProfile> RetrieveWithPropertiesByUserIdAsync(Guid userId)
        {
            var patientId = await RetrieveByUserIdAsync(userId);
            var res = await RetrieveWithAllPropertiesAsync(patientId.Id);
            return res;
        }
    }
}
