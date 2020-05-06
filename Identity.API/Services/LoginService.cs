using Identity.API.Models;
using Identity.API.Services.PasswordHashing;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class LoginService : ILoginService<IdentityUser, Guid>
    {
        private readonly ICustomPasswordHasher _hasher;
        private readonly IUserManager<IdentityUser, Guid> _userManager;
        private readonly IPersonalInformationRepository<PersonalInfo, Guid> _personalInformationRepository;//TODO: remove repos and use some method in UserManager

        public LoginService(ICustomPasswordHasher hasher, IUserManager<IdentityUser, Guid> userManager, IPersonalInformationRepository<PersonalInfo, Guid> personalInformationRepository)
        {
            this._hasher = hasher;
            this._userManager = userManager;
            this._personalInformationRepository = personalInformationRepository;
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var info = await _personalInformationRepository.GetByEmailAsync(email);
            return info != null && _hasher.Verify(info.HashedPassword, password);
        }
        public async Task<bool> ValidateCredentials(IdentityUser user, string password)
        {
            var info = await _personalInformationRepository.RetrieveAsync(user.PersonalInformationId);
            return info != null && _hasher.Verify(info.HashedPassword, password);
        }
    }
}
