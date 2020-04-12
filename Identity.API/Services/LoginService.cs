using Identity.API.Models;
using Identity.API.Services.PasswordHashing;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class LoginService : ILoginService<IdentityUser, Guid>
    {        
        private readonly ICustomPasswordHasher hasher;
        private readonly IUserManager<IdentityUser, Guid> userManager;

        public LoginService(ICustomPasswordHasher hasher, IUserManager<IdentityUser, Guid> userManager)
        {
            this.hasher = hasher;
            this.userManager = userManager;
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user != null && hasher.Verify(user.PersonalInformation?.HashedPassword, password);
        }
        public bool ValidateCredentials(IdentityUser user, string password)
        {
            return hasher.Verify(user?.PersonalInformation?.HashedPassword, password);
        }
    }
}
