using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IControlRequestRepository : IAsyncCRUDRepository<ControlRequest, Guid>
    {
        Task<bool> ExistPendingRequest(Guid watchmanId, Guid patientId);
        Task<IEnumerable<ControlRequest>> FindPendingRequests(Guid patientId);
    }
}
