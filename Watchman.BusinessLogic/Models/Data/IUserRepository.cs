
using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IUserRepository<TUser> : IUserRepository<TUser, Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
        where TUser : IUser
    { }
    public interface IUserRepository<TUser, TKey, TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey, TPersonalInfoKey> : IAsyncCRUDRepository<TUser, TKey>
        where TUser : IUser<TKey, TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey, TPersonalInfoKey>
        where TKey : IEquatable<TKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
        where TPersonalInfoKey : IEquatable<TPersonalInfoKey>
    {
        Task<TUser> GetByEmailAsync(string email);
    }
}
