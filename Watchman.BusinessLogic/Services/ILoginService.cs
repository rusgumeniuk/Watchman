
using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models;

namespace Watchman.BusinessLogic.Services
{
    public interface ILoginService<TUser, TKey>
        where TUser : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<bool> ValidateCredentialsAsync(string email, string password);
        Task<bool> ValidateCredentials(TUser user, string password);
    }
}
