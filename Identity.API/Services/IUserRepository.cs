using Identity.API.Models;

using System;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IUserRepository : IRepository<WatchmanUser, Guid>
    {
        Task<WatchmanUser> GetByEmail(string email);
    }
}
