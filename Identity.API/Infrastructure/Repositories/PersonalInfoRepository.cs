using Identity.API.Data;
using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using Watchman.API.Common.Infrastructure.Repositories;
using Watchman.BusinessLogic.Models.Data;

namespace Identity.API.Infrastructure.Repositories
{
    public class PersonalInfoRepository : Repository<PersonalInfo, Guid>, IPersonalInformationRepository<PersonalInfo, Guid>
    {
        public WatchmanDbContext WatchmanContext => _context as WatchmanDbContext;
        public PersonalInfoRepository(WatchmanDbContext context) : base(context) { }

        public override async Task<PersonalInfo> RetrieveWithAllPropertiesAsync(Guid id)
        {
            return await RetrieveAsync(id);
        }

        public async Task<PersonalInfo> GetByEmailAsync(string email, string token = null)
        {
            return await
                WatchmanContext
                .PersonalInformation
                .FirstOrDefaultAsync(info => info.Email.Equals(email));
        }

        public async Task<Guid> GetIdByEmailAsync(string email, string token = null)
        {
            var info = await
                WatchmanContext
                .PersonalInformation
                .FirstOrDefaultAsync(info => info.Email.Equals(email));

            return info.Id;
        }
    }
}
