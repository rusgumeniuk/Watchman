using Identity.API.Models;
using Identity.API.Services.PasswordHashing;

using System;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;

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

        public void Register(string email, string password)
        {
            Register(new PersonalInfo() { Email = email, HashedPassword = Hasher.Hash(password) });
        }
        public void Register(PersonalInformation personalInformation)
        {
            Register(new WatchmanUser() { PersonalInformation = personalInformation as PersonalInformation });
        }
        public void Register(IUser user)
        {
            if (!AreCredentialsNotEmpty(user.PersonalInformation.Email, user.PersonalInformation.HashedPassword))
                throw new ArgumentNullException("Please input valid credentials");
            if (UserRepository.GetByEmailAsync(user.PersonalInformation.Email).Result == null)
            {
                UserRepository.Create(user as WatchmanUser);
                UserRepository.SaveChanges();
            }
            else
                throw new ArgumentException($"User with email '{user.PersonalInformation.Email}' already exist");
        }

        public WatchmanUser FindByEmail(string email)
        {
            return UserRepository.GetByEmailAsync(email).Result as WatchmanUser;
        }
        public WatchmanUser FindById(Guid key)
        {
            return UserRepository.Retrieve(key) as WatchmanUser;
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
