using Identity.API.Models;
using Identity.API.Services.PasswordHashing;
using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class UserManager : IUserManager<IdentityUser, Guid>
    {
        private readonly IUserRepository<IdentityUser> UserRepository;
        private readonly ICustomPasswordHasher Hasher;

        public UserManager(IUserRepository<IdentityUser> userRepository, ICustomPasswordHasher hasher)
        {
            this.UserRepository = userRepository;
            this.Hasher = hasher;
        }

        public async Task RegisterAsync(PersonalInformation personalInformation, string clearPassword)
        {
            await RegisterAsync(new IdentityUser() { PersonalInformation = personalInformation as PersonalInformation }, clearPassword);
        }
        public async Task RegisterAsync(User<Guid> user, string clearPassword)
        {
            if (UserRepository.GetByEmailAsync(user.PersonalInformation.Email).Result == null)
            {
                user.PersonalInformation.HashedPassword = Hasher.Hash(clearPassword);
                await UserRepository.CreateAsync(user as IdentityUser);
                await UserRepository.SaveChangesAsync();
            }
            else
                throw new ArgumentException($"User with email '{user.PersonalInformation.Email}' already exist");
        }

        public async Task<IdentityUser> FindByEmailAsync(string email, string token = null)
        {
            return await UserRepository.GetByEmailAsync(email);
        }
        public async Task<IdentityUser> FindByIdAsync(Guid key, string token = null)
        {
            return await UserRepository.RetrieveAsync(key);
        }
    }
}
