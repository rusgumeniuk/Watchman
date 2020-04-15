using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Services
{
    public interface IPersonalInformationService<TInfo, TKey> : IDisposable
        where TInfo : PersonalInformation<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TInfo> GetPersonalInformation(TKey key, string token = null);

        Task<TInfo> UpdatePersonalInformation(TInfo obj, TKey key = default, string token = null);
    }
}
