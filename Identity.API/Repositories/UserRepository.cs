using Identity.API.Data;
using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;

namespace Identity.API.Repositories
{
    public class UserRepository : Repository<WatchmanUser, Guid>, IUserRepository<WatchmanUser>
    {
        public WatchmanDbContext WatchmanContext => Context as WatchmanDbContext;
        public UserRepository(WatchmanDbContext context) : base(context) { }

        public async Task CreateAsync(WatchmanUser entity)
        {
            await Context.Set<WatchmanUser>().AddAsync(entity);
        }

        public async Task<WatchmanUser> GetByEmailAsync(string email)
        {
            return await WatchmanContext.Set<WatchmanUser>()
                .Include(user => user.PersonalInformation)
                .FirstOrDefaultAsync(user => user.PersonalInformation.Email.Equals(email));
        }

        public async Task<WatchmanUser> RetrieveAsync(Guid id)
        {
            return await Context.Set<WatchmanUser>().FindAsync(id);
        }

        public override WatchmanUser RetrieveWithAllProperties(Guid id)
        {
            return WatchmanContext
                .Users
                .Include(user => user.Patient)
                .Include(user => user.PersonalInformation)
                .Include(user => user.Watchman)
                .First(user => user.Id.Equals(id));
        }
        public async Task<WatchmanUser> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await WatchmanContext
                .Users
                .Include(user => user.Patient)
                .Include(user => user.PersonalInformation)
                .Include(user => user.Watchman)
                .FirstAsync(user => user.Id.Equals(id));
        }

        public async Task<IEnumerable<WatchmanUser>> RetrieveAll()
        {
            return await Context.Set<WatchmanUser>().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await WatchmanContext.SaveChangesAsync();
        }
        public async Task DisposeAsync()
        {
            await WatchmanContext.DisposeAsync();
        }        
    }
}
