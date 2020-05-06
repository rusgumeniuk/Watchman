using Identity.API.Models;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class RoleService : IRoleService<Guid>
    {
        private readonly IUserRepository<IdentityUser> _userRepository;
        private readonly IPersonalInformationRepository<PersonalInfo, Guid> _infoRepository;

        public RoleService(IUserRepository<IdentityUser> userRepository, IPersonalInformationRepository<PersonalInfo, Guid> personalInformationRepository)
        {
            this._userRepository = userRepository;
            this._infoRepository = personalInformationRepository;
        }

        public async Task<string> GetRoleByUser(Guid userId)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            var info = await _infoRepository.RetrieveAsync(user.PersonalInformationId);
            return info.Roles;
        }

        public async Task<string> GetRoleByUser(string userEmail)
        {
            var info = await _infoRepository.GetByEmailAsync(userEmail);
            return info.Roles;
        }
    }
}
