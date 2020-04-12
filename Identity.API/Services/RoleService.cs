using Identity.API.Models;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class RoleService : IRoleService<Guid>
    {
        private readonly IUserRepository<IdentityUser> userRepository;

        public RoleService(IUserRepository<IdentityUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<string> GetRoleByUser(Guid userId)
        {
            var user = await userRepository.RetrieveWithAllPropertiesAsync(userId);//TODO: create simple method instead of RetrieveWithAllProps
            return user.PersonalInformation.Roles;
        }

        public async Task<string> GetRoleByUser(string userEmail)
        {
            var user = await userRepository.GetByEmailAsync(userEmail);
            return user.PersonalInformation.Roles;
        }
    }
}
