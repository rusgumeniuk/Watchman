using Identity.API.Models;
using Identity.API.Services.PasswordHashing;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Services
{
    public class LoginService : ILoginService<WatchmanUser, Guid>
    {
        private readonly IUserRepository<WatchmanUser> UserRepository;
        private readonly ICustomPasswordHasher Hasher;

        public LoginService(IUserRepository<WatchmanUser> userRepository, ICustomPasswordHasher hasher)
        {
            this.UserRepository = userRepository;
            this.Hasher = hasher;
        }

        public async Task RegisterAsync(string email, string password)
        {
            await RegisterAsync(new PersonalInfo() { Email = email, HashedPassword = Hasher.Hash(password) });
        }
        public async Task RegisterAsync(PersonalInformation personalInformation)
        {
            await RegisterAsync(new WatchmanUser() { PersonalInformation = personalInformation as PersonalInformation });
        }
        public async Task RegisterAsync(IUser user)
        {
            if (!AreCredentialsNotEmpty(user.PersonalInformation.Email, user.PersonalInformation.HashedPassword))
                throw new ArgumentNullException("Please input valid credentials");
            if (UserRepository.GetByEmailAsync(user.PersonalInformation.Email).Result == null)
            {
                await UserRepository.CreateAsync(user as WatchmanUser);
                await UserRepository.SaveChangesAsync();
            }
            else
                throw new ArgumentException($"User with email '{user.PersonalInformation.Email}' already exist");
        }

        public async Task<WatchmanUser> FindByEmailAsync(string email)
        {
            return await UserRepository.GetByEmailAsync(email);
        }
        public async Task<WatchmanUser> FindByIdAsync(Guid key)
        {
            return await UserRepository.RetrieveAsync(key);
        }

        public bool ValidateCredentials(WatchmanUser user, string password)
        {
            return Hasher.Verify(user?.PersonalInformation?.HashedPassword, password);
        }
        private bool AreCredentialsNotEmpty(string email, string password)
        {
            return !String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(password);
        }
    }
}
