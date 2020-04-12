using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public abstract class User<TKey> : User<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>
        where TKey : IEquatable<TKey>
    { }
    public abstract class User<TKey, TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey, TPersonalInfoKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
    {
        public TKey Id { get; set; }
        public Patient<TKey> Patient { get; set; }
        public WatchmanProfile<TWatchmanKey> Watchman { get; set; }
        public PersonalInformation<TKey> PersonalInformation { get; set; }
    }
}
