using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IPersonalInformationRepository<TPersonalInformation, TKey> : IAsyncCRUDRepository<TPersonalInformation, TKey>
        where TPersonalInformation : PersonalInformation<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TPersonalInformation> GetByEmailAsync(string email, string token = null);
        Task<TKey> GetIdByEmailAsync(string email, string token = null);
    }
}
