using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Services
{
    public interface IControlRequestService
    {
        Task UpdateRequestStatus(Guid requestId, ControlRequestStatus status, string token = null);
        Task<bool> ExistPendingRequest(Guid watchmanId, Guid patientId, string token = null);
        Task CreateControlRequest(Guid watchmanId, Guid patientId, string token = null);
        Task<IEnumerable<ControlRequest>> GetPendingRequests(Guid patientId, string token = null);
        Task<ControlRequest> GetControlRequest(Guid requestId, string token = null);
    }
}