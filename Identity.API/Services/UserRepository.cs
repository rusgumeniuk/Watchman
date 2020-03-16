using Identity.API.Data;
using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class UserRepository : Repository<WatchmanUser, Guid>, IUserRepository
    {
        public WatchmanDbContext WatchmanContext => Context as WatchmanDbContext;
        public UserRepository(WatchmanDbContext context) : base(context) { }

        public async Task<WatchmanUser> GetByEmail(string email)
        {
            return await WatchmanContext.Set<WatchmanUser>()
                .Include(user => user.PersonalInformation)
                .FirstOrDefaultAsync(user => user.PersonalInformation.Email.Equals(email));
        }
    }
}
