using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace HealthService.API.Services
{

    public class ControlRequestService : IControlRequestService
    {
        private readonly IControlRequestRepository _repository;

        public ControlRequestService(IControlRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task UpdateRequestStatus(Guid requestId, ControlRequestStatus status, string token = null)
        {
            var request = await _repository.RetrieveAsync(requestId);
            if (request.Status <= status)
            {
                request.Status = status;
                request.ConsiderationTime = DateTime.Now;

                await _repository.UpdateAsync(request);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistPendingRequest(Guid watchmanId, Guid patientId, string token = null)
        {
            return await _repository.ExistPendingRequest(watchmanId, patientId);
        }

        public async Task CreateControlRequest(Guid watchmanId, Guid patientId, string token = null)
        {
            if (!(await ExistPendingRequest(watchmanId, patientId)))
            {
                ControlRequest controlRequest = new ControlRequest()
                {
                    WatchmanId = watchmanId,
                    PatientId = patientId
                };
                await _repository.CreateAsync(controlRequest);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ControlRequest>> GetPendingRequests(Guid patientId, string token = null)
        {
            return await _repository.FindPendingRequests(patientId);
        }

        public async Task<ControlRequest> GetControlRequest(Guid requestId, string token = null)
        {
            return await _repository.RetrieveAsync(requestId);
        }
    }
}
