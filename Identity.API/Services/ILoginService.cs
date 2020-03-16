
using Identity.API.Models;

using System;

using Watchman.BusinessLogic.Models;
using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Services
{
    public interface ILoginService<TUser, TKey>
        where TUser : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        bool ValidateCredentials(TUser user, string password);

        TUser FindByEmail(string email);
        TUser FindById(TKey key);

        void Register(string email, string password);
        void Register(IPersonalInformation<Guid> personalInformation);
        void Register(IUser<PersonalInformation> user);
    }
}
