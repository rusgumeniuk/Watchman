using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public interface IUser<TPersInfo> : IUser<TPersInfo, Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
    where TPersInfo : IPersonalInformation<Guid>
    { }
    public interface IUser<TPersInfo, TKey, TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey, TPersonalInfoKey> : IIdentifiedEntity<TKey>
        where TPersInfo : IPersonalInformation<TPersonalInfoKey>
        where TKey : IEquatable<TKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
        where TPersonalInfoKey : IEquatable<TPersonalInfoKey>
    {
        IPatient<TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey> Patient { get; set; }
        IWatchman<TWatchmanKey> Watchman { get; set; }
        TPersInfo PersonalInformation { get; set; }
    }
}
