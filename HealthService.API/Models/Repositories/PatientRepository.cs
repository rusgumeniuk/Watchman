using HealthService.API.Models.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Repositories
{
    public class PatientRepository : Repository<PatientProfile, Guid>, IPatientRepository<PatientProfile, Guid>
    {
        public HealthDbContext HealthContext => Context as HealthDbContext;
        public PatientRepository(HealthDbContext context) : base(context) { }

        public HealthMeasurement<Guid, Guid> GetLastHealthMeasurement(Guid patientId)
        {
            var patient = HealthContext
                .Patients
                .Include(pat => pat.HealthMeasurements)
                .First(pat => pat.Id.Equals(patientId));
            return patient.HealthMeasurements.ElementAt(GetIndexOfItemWithNewDate(patient.HealthMeasurements));
        }        
        public IEnumerable<HealthMeasurement<Guid, Guid>> GetLastHealthMeasurements(Guid patientId, int count)
        {
            var patient = HealthContext
                .Patients
                .Include(pat => pat.HealthMeasurements)
                .First(pat => pat.Id.Equals(patientId));
            return patient.HealthMeasurements.TakeLast(count);
        }
        public void AddHealthMeasurement(Guid patientId, HealthMeasurement<Guid, Guid> healthMeasurement)
        {
            var patient = HealthContext
                .Patients
                .Include(pat => pat.HealthMeasurements)
                .First(pat => pat.Id.Equals(patientId));
            patient.HealthMeasurements.Add(healthMeasurement);
        }


        public void AddPatientToUser(Guid userId, PatientProfile patient = null)
        {
            var user = HealthContext.Users.Find(userId);
            user.Patient = patient ?? new PatientProfile();
        }
        public void AddPatientToUser<TUser>(TUser user, PatientProfile patient = null) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            AddPatientToUser(user.Id, patient);
        }

        public bool ExistPatientProfile(Guid userId)
        {
            var patient = HealthContext
                .Users
                .Include(us => us.Patient)
                .FirstOrDefault(us => us.Id.Equals(userId))
                .Patient;
            return patient != null;
        }
        public bool ExistPatientProfile<TUser>(TUser user) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            return ExistPatientProfile(user.Id);
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

        public IEnumerable<WatchmanProfile<Guid>> GetWatchmenOfPatient(PatientProfile patient)
        {
            return HealthContext
                .WatchmanPatients
                .Where(pair => pair.PatientId.Equals(patient.Id))
                .Select(pair => pair.Watchman);
        }

        public async Task CreateAsync(PatientProfile entity)
        {
            await Context.Set<PatientProfile>().AddAsync(entity);
        }

        public async Task<PatientProfile> RetrieveAsync(Guid id)
        {
            return await Context.Set<PatientProfile>().FindAsync(id);
        }
        public async Task<IEnumerable<PatientProfile>> RetrieveAll()
        {
            return await Context.Set<PatientProfile>().ToListAsync();
        }


        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        public async Task DisposeAsync()
        {
            await Context.DisposeAsync();
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
