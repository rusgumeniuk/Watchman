using Microsoft.AspNetCore.Authentication;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models;
namespace Identity.API.Services
{
    public interface ILoginService<T, TKey>
        where T : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<bool> ValidateCredentials(T user, string password);

        Task<T> FindByEmail(string email);
        Task<T> FindById(TKey key);

        Task SignIn(T user);

        Task SignInAsync(T user, AuthenticationProperties properties, string authenticationMethod = null);
    }
}
