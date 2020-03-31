
using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Services
{
    public interface ILoginService<TUser, TKey>
        where TUser : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        bool ValidateCredentials(TUser user, string password);

        TUser FindByEmail(string email);
        TUser FindById(TKey key);

        void Register(string email, string password);
        void Register(PersonalInformation personalInformation);
        void Register(IUser user);

        Task<string> GetToken(string email, string password);
    }
}
