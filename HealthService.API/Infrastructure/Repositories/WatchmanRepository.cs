﻿using HealthService.API.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Infrastructure.Repositories
{
    public class WatchmanRepository : Repository<WatchmanProfileHealth, Guid>, IWatchmanRepository<WatchmanProfileHealth, Guid>
    {
        public HealthDbContext HealthContext => Context as HealthDbContext;
        public WatchmanRepository(HealthDbContext context) : base(context) { }

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
    }
}