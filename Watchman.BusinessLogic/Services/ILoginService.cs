
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

        Task<TUser> FindByEmailAsync(string email);
        Task<TUser> FindByIdAsync(TKey key);

        Task RegisterAsync(string email, string password);
        Task RegisterAsync(PersonalInformation personalInformation);
        Task RegisterAsync(IUser user);
    }
}
