﻿
using Newtonsoft.Json;

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
    public class WatchmanPatientService : IUserWatchmanPatientService<Guid>, IWatchmanPatientService<Guid>
    {
        private const string PatientUrl = "https://localhost:44383/patient";
        private const string WatchmanUrl = "https://localhost:44383/watchman";
        private const string UserUrl = "https://localhost:44383/user";
        private readonly IHttpClient _client;

        public WatchmanPatientService(IHttpClient httpClient)
        {
            this._client = httpClient;
        }

        public Task AddHealthMeasurementAsync(Guid patientId, HealthMeasurement<Guid, Guid> healthMeasurement, string token = null)
        {
            throw new NotImplementedException();
        }

        public Task AddIgnorableSignToPatientAsync(Guid patientId, Sign<Guid> sign, string token = null)
        {
            throw new NotImplementedException();
        }

        public async Task AddPatientToUserAsync(Guid userId, Guid patientId, string token = null)
        {
            var uri = UserUrl + "/AddPatientToUser";
            var obj = new { UserId = userId, SecondId = patientId };

            await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);
        }

        public async Task AddWatchmanToUserAsync(Guid userId, Guid watchmanId, string token = null)
        {
            var uri = UserUrl + "/AddWatchmanToUser";
            var obj = new { UserId = userId, SecondId = watchmanId };

            await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);
        }

        async Task IUserWatchmanPatientService<Guid>.RemovePatientFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        async Task IUserWatchmanPatientService<Guid>.RemoveWatchmanFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientToWatchmanAsync(Guid watchmanId, Guid patientId, string token = null)
        {
            return null;
        }

        public Task AddWatchmanToUserAsync(Guid userId, Guid watchmanId)
        {
            throw new NotImplementedException();
        }

        public Task<IAnalysisResult> AnalyzeLastMeasurementAsync(Guid patientId, string token = null)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateIfNotExistPatientAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateIfNotExistWatchmanAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreatePatientAsync(Patient<Guid> patient, string token = null)
        {
            var uri = PatientUrl + "/Create";
            var obj = new { Id = patient.Id };

            await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);

        }

        public async Task CreateWatchmanAsync(WatchmanProfile<Guid> watchman, string token = null)
        {
            var uri = WatchmanUrl + "/Create";
            var obj = new { Id = watchman.Id };

            await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);
        }

        public Task<bool> ExistPatientAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistWatchmanAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<HealthMeasurement<Guid, Guid>> GetLastHealthMeasurementAsync(Guid patientId, string token = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HealthMeasurement<Guid, Guid>>> GetLastHealthMeasurementsAsync(Guid patientId, int count, string token = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient<Guid>> GetPatientAsync(Guid id, string token = null)
        {
            var uri = PatientUrl + "/get";
            var obj = new { Id = id };

            var response = await _client.SendRequest(HttpMethod.Get, null, obj, uri, token);
            var result = await _client.GetResponseResult(response);

            var res = !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? null : JsonConvert.DeserializeObject<PatientInfo>(result);

            return res;
        }

        public Task<Patient<Guid>> GetPatientByUserIdAsync(Guid usedId)
        {
            throw new NotImplementedException();
        }

        public Task<Patient<Guid>> GetPatientWithPropertiesByUserIdAsync(Guid usedId)
        {
            throw new NotImplementedException();
        }

        public async Task<WatchmanProfile<Guid>> GetWatchmanAsync(Guid id, string token = null)
        {
            var uri = WatchmanUrl + "/get";
            var obj = new { Id = id };

            var response = await _client.SendRequest(HttpMethod.Get, null, obj, uri, token);
            var result = await _client.GetResponseResult(response);

            var res = !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? null : JsonConvert.DeserializeObject<WatchmanInfo>(result);

            return res;
        }

        public Task<WatchmanProfile> GetWatchmanByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<WatchmanProfile> GetWatchmanWithPropertiesByUserIdAsync(Guid userId)
        {
            var uri = $"{WatchmanUrl}/GetWatchmantWithPropsByUserId";
            var obj = new { Id = userId };
            var response = await _client.SendRequest(HttpMethod.Get, null, obj, uri);
            var dto = await _client.GetResponseResultOrDefault<WatchmanDTO>(response);
            if (dto == null)
                return null;
            var watchman = new WatchmanInfo()
            {
                Id = dto.Id,
                WatchmanPatients = dto.WatchmanPatients.ToList<WatchmanPatient<Guid, Guid>>()
            };

            return watchman;
        }

        public void RemoveAllPatientFromWatchman(Guid watchmanId, string token = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllWatchmenFromPatient(Guid patientId, string token = null)
        {
            throw new NotImplementedException();
        }

        public void RemovePatientFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePatientFromWatchmanAsync(Guid watchmanId, Guid patientId, string token = null)
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
