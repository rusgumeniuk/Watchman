using Identity.API.Models;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class PersonalInfoService : IPersonalInformationService<PersonalInfo, Guid>
    {
        private readonly IPersonalInformationRepository<PersonalInfo, Guid> _repository;
        public PersonalInfoService(
            IPersonalInformationRepository<PersonalInfo, Guid> personalInformationRepository
            )
        {
            this._repository = personalInformationRepository;
        }

        public async Task<PersonalInfo> GetPersonalInformation(Guid key, string token = null)
        {
            var info = await _repository.RetrieveAsync(key);
            info.HashedPassword = null;
            return info;
        }

        public async Task<PersonalInfo> UpdatePersonalInformation(PersonalInfo obj, Guid key = default, string token = null)
        {
            return await _repository.UpdateAsync(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
