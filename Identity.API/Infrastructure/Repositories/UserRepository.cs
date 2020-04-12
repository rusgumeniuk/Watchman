using Identity.API.Data;
using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;

namespace Identity.API.Infrastructure.Repositories
{
    public class UserRepository : Repository<IdentityUser, Guid>, IUserRepository<IdentityUser>
    {
        public WatchmanDbContext WatchmanContext => Context as WatchmanDbContext;
        public UserRepository(WatchmanDbContext context) : base(context) { }

        public async Task<IdentityUser> GetByEmailAsync(string email)
        {
            return await WatchmanContext.Set<IdentityUser>()
                .Include(user => user.PersonalInformation)
                .FirstOrDefaultAsync(user => user.PersonalInformation.Email.Equals(email));
        }

        public async override Task<IdentityUser> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await
                WatchmanContext
                .Users
                .Include(user => user.Patient)
                    .ThenInclude(pat => pat.CurrentActivityState)
                .Include(user => user.Patient)
                  .ThenInclude(pat => pat.HealthMeasurements)
                    .ThenInclude(meas => meas.Signs)
                .Include(user => user.PersonalInformation)
                .Include(user => user.Watchman)
                .FirstAsync(user => user.Id.Equals(id));
        }
    }
}
