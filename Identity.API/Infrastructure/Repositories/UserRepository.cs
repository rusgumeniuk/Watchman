using Identity.API.Data;
using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using Watchman.API.Common.Infrastructure.Repositories;
using Watchman.BusinessLogic.Models.Data;

namespace Identity.API.Infrastructure.Repositories
{
    public class UserRepository : Repository<IdentityUser, Guid>, IUserRepository<IdentityUser>
    {
        public WatchmanDbContext WatchmanContext => Context as WatchmanDbContext;
        public UserRepository(WatchmanDbContext context) : base(context) { }

        public async override Task<IdentityUser> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await
                WatchmanContext
                .Users
                .FirstAsync(user => user.Id.Equals(id));
        }

        public async Task<IdentityUser> GetByPatientId(Guid patientId)
        {
            return await
                WatchmanContext
                .Users
                .FirstAsync(user => user.PatientId.Equals(patientId));
        }

        public async Task<IdentityUser> GetByWatchmanId(Guid watchmanId)
        {
            return await
                WatchmanContext
                .Users
                .FirstAsync(user => user.WatcmanId.Equals(watchmanId));
        }

        public async Task<IdentityUser> GetByPersonalInformationId(Guid personalInfoId)
        {
            return await
                WatchmanContext
                .Users
                .FirstAsync(user => user.PersonalInformationId.Equals(personalInfoId));
        }
    }
}
