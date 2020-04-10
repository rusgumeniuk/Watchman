
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Services
{
    public class WatchmanPatientService : IWatchmanPatientService<Guid>
    {
        private const string patientUrl = "https://localhost:44383/patient";
        private const string watchmanUrl = "https://localhost:44383/watchman";
        private readonly IHttpClient client;

        public WatchmanPatientService(IHttpClient httpClient)
        {
            this.client = httpClient;
        }

        public Task AddHealthMeasurementAsync(Guid patientId, HealthMeasurement<Guid, Guid> healthMeasurement)
        {
            throw new NotImplementedException();
        }

        public Task AddIgnorableSignToPatientAsync(Guid patientId, Sign<Guid> sign)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientToUserAsync(Guid userId, Patient<Guid> patient = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientToWatchmanAsync(Guid watchmanId, Guid patientId)
        {
            throw new NotImplementedException();
        }

        public Task AddWatchmanToUserAsync(Guid userId, WatchmanProfile watchman = null)
        {
            throw new NotImplementedException();
        }

        public Task<IAnalysisResult> AnalyzeLastMeasurementAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public Task CreateIfNotExistPatientAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateIfNotExistWatchmanAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistPatientAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistWatchmanAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<HealthMeasurement<Guid, Guid>> GetLastHealthMeasurementAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HealthMeasurement<Guid, Guid>>> GetLastHealthMeasurementsAsync(Guid patientId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<Patient<Guid>> GetPatientByUserIdAsync(Guid usedId)
        {
            throw new NotImplementedException();
        }

        public Task<Patient<Guid>> GetPatientWithPropertiesByUserIdAsync(Guid usedId)
        {
            throw new NotImplementedException();
        }

        public Task<WatchmanProfile> GetWatchmanByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<WatchmanProfile> GetWatchmanWithPropertiesByUserIdAsync(Guid userId)
        {
            var uri = $"{watchmanUrl}/GetWatchmantWithPropsByUserId";
            var obj = new { Id = userId };
            var response = await client.SendRequest(HttpMethod.Get, null, obj, uri);
            var dto = await client.GetResponseResultOrDefault<WatchmanDTO>(response);
            if (dto == null)
                return null;
            var watchman = new WatchmanInfo()
            {
                Id = dto.Id,
                WatchmanPatients = dto.WatchmanPatients.ToList<WatchmanPatient<Guid, Guid>>()
            };

            return watchman;
        }

        public void RemoveAllPatientFromWatchman(Guid watchmanId)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllWatchmenFromPatient(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public void RemovePatientFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePatientFromWatchmanAsync(Guid watchmanId, Guid patientId)
        {
            throw new NotImplementedException();
        }

        public void RemoveWatchmanFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }



    }
}

class WatchmanDTO
{
    public Guid Id { get; set; }
    public ICollection<WatchmanPatientImpl> WatchmanPatients { get; set; }

}

class WatchmanPatientImpl : WatchmanPatient<Guid, Guid> { }
