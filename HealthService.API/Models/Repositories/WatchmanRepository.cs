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

        public async Task AddWatchmanToUserAsync(Guid userId, WatchmanProfileHealth watchman = null)
        {
            var user = await HealthContext.Users.FindAsync(userId);
            user.Watchman = watchman ?? new WatchmanProfileHealth();
        }
        public async Task AddWatchmanToUserAsync<TUser>(TUser user, WatchmanProfileHealth watchman = null) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            await AddWatchmanToUserAsync(user.Id, watchman);
        }

        public async Task<bool> ExistWatchmanProfileAsync(Guid userId)
        {
            var user = await HealthContext
                .Users
                .Include(us => us.Watchman)
                .FirstOrDefaultAsync(us => us.Id.Equals(userId));
            return user?.Watchman != null;
        }
        public async Task<bool> ExistWatchmanProfileAsync<TUser>(TUser user) where TUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        {
            return await ExistWatchmanProfileAsync(user.Id);
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

        public async Task<IEnumerable<Patient<Guid>>> GetPatientsAsync(WatchmanProfileHealth watchman)
        {
            return await HealthContext
                .WatchmanPatients
                .Where(pair => pair.WatchmanId.Equals(watchman.Id))
                .Select(pair => pair.Patient)
                .ToListAsync();
        }


        public async override Task<WatchmanProfileHealth> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await HealthContext
                .Watchmen
                .Include(watch => watch.WatchmanPatients)
                .FirstAsync(watchm => watchm.Id.Equals(id));
        }



        public async Task<WatchmanProfileHealth> RetrieveByUserIdAsync(Guid userId)
        {
            var res = await HealthContext
            .Users
            .Include(user => user.Watchman)
            .FirstAsync(user => user.Id.Equals(userId));
            return res.Watchman as WatchmanProfileHealth;
        }

        public async Task<WatchmanProfileHealth> RetrieveWithPropertiesByUserIdAsync(Guid userId)
        {
            var watchmanId = await RetrieveByUserIdAsync(userId);
            return await RetrieveWithAllPropertiesAsync(watchmanId.Id);
        }
    }
}
