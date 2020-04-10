using Identity.API.Models;
using Identity.API.Services.PasswordHashing;
using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class UserManager : IUserManager<WatchmanUser, Guid>
    {
        private readonly IUserRepository<WatchmanUser> UserRepository;
        private readonly ICustomPasswordHasher Hasher;

        public UserManager(IUserRepository<WatchmanUser> userRepository, ICustomPasswordHasher hasher)
        {
            this.UserRepository = userRepository;
            this.Hasher = hasher;
        }

        public async Task RegisterAsync(PersonalInformation personalInformation, string clearPassword)
        {
            await RegisterAsync(new WatchmanUser() { PersonalInformation = personalInformation as PersonalInformation }, clearPassword);
        }
        public async Task RegisterAsync(IUser user, string clearPassword)
        {
            if (UserRepository.GetByEmailAsync(user.PersonalInformation.Email).Result == null)
            {
                user.PersonalInformation.HashedPassword = Hasher.Hash(clearPassword);
                await UserRepository.CreateAsync(user as WatchmanUser);
                await UserRepository.SaveChangesAsync();
            }
            else
                throw new ArgumentException($"User with email '{user.PersonalInformation.Email}' already exist");
        }

        public async Task<WatchmanUser> FindByEmailAsync(string email, string token = null)
        {
            return await UserRepository.GetByEmailAsync(email);
        }
        public async Task<WatchmanUser> FindByIdAsync(Guid key, string token = null)
        {
            return await UserRepository.RetrieveAsync(key);
        }
    }
}
