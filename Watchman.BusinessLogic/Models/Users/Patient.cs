using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Users
{
    public abstract class Patient<TKey> : Patient<TKey, TKey, TKey, TKey, TKey, TKey>
    where TKey : IEquatable<TKey>
    { }
    public abstract class Patient<TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey> : IIdentifiedEntity<TPatientKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
    {
        public TPatientKey Id { get; set; }
        public ICollection<HealthMeasurement<THealthMeasurementKey, TSignKey>> HealthMeasurements { get; set; }

        public ICollection<PatientSign<TPatientKey, ushort>> IgnorableSignPair { get; set; }
        public ICollection<WatchmanPatient<TWatchmanKey, TPatientKey>> WatchmanPatients { get; set; } 
    }
}
