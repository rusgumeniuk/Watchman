using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Services
{
    public interface IUserManager<TUser, TKey>
        where TUser : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TUser> FindByEmailAsync(string email);
        Task<TUser> FindByIdAsync(TKey key);

        Task RegisterAsync(PersonalInformation personalInformation, string clearPassword);
        Task RegisterAsync(IUser user, string clearPassword);
    }
}
