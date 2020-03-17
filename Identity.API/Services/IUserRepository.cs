using Identity.API.Models;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;

namespace Identity.API.Services
{
    public interface IUserRepository : IAsyncCRUDRepository<WatchmanUser, Guid>
    {
        Task<WatchmanUser> GetByEmailAsync(string email);
    }
}
