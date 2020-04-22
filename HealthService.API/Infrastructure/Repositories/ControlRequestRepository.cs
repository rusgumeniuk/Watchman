using HealthService.API.Data;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.API.Common.Infrastructure.Repositories;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Infrastructure.Repositories
{
    public class ControlRequestRepository : Repository<ControlRequest, Guid>, IControlRequestRepository
    {
        private HealthDbContext HealthContext => _context as HealthDbContext;
        public ControlRequestRepository(HealthDbContext context) : base(context) { }

        public override async Task<ControlRequest> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await RetrieveAsync(id);
        }

        public async Task<bool> ExistPendingRequest(Guid watchmanId, Guid patientId)
        {
            return await
                HealthContext
                    .ControlRequests
                    .FirstOrDefaultAsync(request =>
                        request.WatchmanId.Equals(watchmanId) &&
                        request.PatientId.Equals(patientId) &&
                        request.Status == ControlRequestStatus.Wait) != null;
        }

        public async Task<IEnumerable<ControlRequest>> FindPendingRequests(Guid patientId)
        {
            return await
                HealthContext
                    .ControlRequests
                    .Where(request =>
                        request.PatientId.Equals(patientId) &&
                        request.Status == ControlRequestStatus.Wait)
                    .ToListAsync();
        }
    }
}
