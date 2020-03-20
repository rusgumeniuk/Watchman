using HealthService.API.Models.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Repositories
{
    public class PatientRepository : Repository<PatientProfile, Guid>, IPatientRepository<PatientProfile, Guid>
    {
        public HealthDbContext HealthContext => Context as HealthDbContext;
        public PatientRepository(HealthDbContext context) : base(context) { }

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
    }
}
