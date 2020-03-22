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
    public class WatchmanRepository : Repository<WatchmanProfileHealth, Guid>, IWatchmanRepository<WatchmanProfileHealth, Guid>
    {
        public HealthDbContext HealthContext => Context as HealthDbContext;
        public WatchmanRepository(HealthDbContext context) : base(context) { }

        public void AddWatchmanToUser(Guid userId, WatchmanProfileHealth watchman = null)
        {
            var user = HealthContext.Users.Find(userId);
            user.Watchman = watchman ?? new WatchmanProfileHealth();
        }
        public void AddWatchmanToUser<TUser>(TUser user, WatchmanProfileHealth watchman = null) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            AddWatchmanToUser(user.Id, watchman);
        }

        public bool ExistWatchmanProfile(Guid userId)
        {
            var watchman = HealthContext
                .Users
                .Include(us => us.Watchman)
                .FirstOrDefault(us => us.Id.Equals(userId))?
                .Watchman;
            return watchman != null;
        }
        public bool ExistWatchmanProfile<TUser>(TUser user) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            return ExistWatchmanProfile(user.Id);
        }

        public void RemoveWatchmanFromUser(Guid userId)
        {
            var user = HealthContext
                .Users
                .Include(us => us.Watchman)
                .FirstOrDefault(us => us.Id.Equals(userId));
            if (user != null && user.Watchman != null)
                user.Watchman = null;
        }
        public void RemoveWatchmanFromUser<TUser>(TUser user) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            RemoveWatchmanFromUser(user.Id);
        }

        public IEnumerable<Patient<Guid>> GetPatients(WatchmanProfileHealth watchman)
        {
            return HealthContext
                .WatchmanPatients
                .Where(pair => pair.WatchmanId.Equals(watchman.Id))
                .Select(pair => pair.Patient);
        }

        public async Task CreateAsync(WatchmanProfileHealth entity)
        {
            await Context.Set<WatchmanProfileHealth>().AddAsync(entity);
        }

        public async Task<WatchmanProfileHealth> RetrieveAsync(Guid id)
        {
            return await Context.Set<WatchmanProfileHealth>().FindAsync(id);
        }

        public override WatchmanProfileHealth RetrieveWithAllProperties(Guid id)
        {
            return HealthContext
                .Watchmen
                .Include(watch => watch.WatchmanPatients)
                .First(watchm => watchm.Id.Equals(id));
        }
        public async Task<WatchmanProfileHealth> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await HealthContext
                .Watchmen
                .Include(watch => watch.WatchmanPatients)
                .FirstAsync(watchm => watchm.Id.Equals(id));
        }

        public async Task<IEnumerable<WatchmanProfileHealth>> RetrieveAll()
        {
            return await Context.Set<WatchmanProfileHealth>().ToListAsync();
        }


        public async Task SaveChangesAsync()
        {
            await HealthContext.SaveChangesAsync();
        }
        public async Task DisposeAsync()
        {
            await HealthContext.DisposeAsync();
        }
    }
}
