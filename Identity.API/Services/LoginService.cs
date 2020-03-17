using Identity.API.Models;
using Identity.API.Repositories;
using Identity.API.Services.PasswordHashing;

using System;

using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Services
{
    public class LoginService : ILoginService<WatchmanUser, Guid>
    {
        private readonly IUserRepository UserRepository;
        private readonly ICustomPasswordHasher Hasher;

        public LoginService(IUserRepository userRepository, ICustomPasswordHasher hasher)
        {
            this.UserRepository = userRepository;
            this.Hasher = hasher;
        }

        public void Register(string email, string password)
        {
            Register(new PersonalInformation() { Email = email, HashedPassword = Hasher.Hash(password) });
        }
        public void Register(IPersonalInformation<Guid> personalInformation)
        {
            Register(new WatchmanUser() { PersonalInformation = personalInformation as PersonalInformation });
        }
        public void Register(IUser<PersonalInformation> user)
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
