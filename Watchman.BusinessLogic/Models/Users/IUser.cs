using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public interface IUser : IUser<Guid, Guid, Guid, Guid, Guid, Guid, Guid, Guid>
    { }
    public interface IUser<TKey, TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey, TPersonalInfoKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
    {
        IPatient<TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey> Patient { get; set; }
        IWatchman<TWatchmanKey> Watchman { get; set; }
        PersonalInformation PersonalInformation { get; set; }
    }
}
