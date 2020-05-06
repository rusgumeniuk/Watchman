using Identity.API.Data;
using Identity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class UserWatchmanPatientService : IUserWatchmanPatientService<Guid>
    {
        private readonly IUserRepository<IdentityUser> _userRepository;
        public UserWatchmanPatientService(IUserRepository<IdentityUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task AddPatientToUserAsync(Guid userId, Guid patientId, string token = null)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            user.PatientId = patientId;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task AddWatchmanToUserAsync(Guid userId, Guid watchmanId, string token = null)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            user.WatcmanId = watchmanId;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistPatientAsync(Guid userId)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            return user?.PatientId != Guid.Empty;
        }

        public Task<bool> ExistWatchmanAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Patient<Guid>> GetPatientByUserIdAsync(Guid usedId)
        {
            throw new NotImplementedException();
        }      

        public Task<WatchmanProfile> GetWatchmanByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }


        public async Task RemovePatientFromUser(Guid userId, string token = null)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            user.PatientId = Guid.Empty;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task RemoveWatchmanFromUser(Guid userId, string token = null)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            user.WatcmanId = Guid.Empty;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
