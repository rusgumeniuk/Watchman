using System;
using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Services
{
    public interface IRoleService<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<string> GetRoleByUser(TKey userId);
        Task<string> GetRoleByUser(string userEmail);
    }
}
