using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Users
{

    public interface IPatient : IPatient<Guid, Guid, Guid, Guid, Guid, Guid> { }
    public interface IPatient<TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey> : IIdentifiedEntity<TPatientKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
    {
        IEnumerable<IHealthMeasurement<THealthMeasurementKey, TSignKey>> HealthMeasurements { get; set; }
        IPatientActivityState<TActivityStateKey> CurrentActivityState { get; set; }
        IPatientHealthState<TPatientHealthKey> CurrentHealthState { get; set; }
        ISet<ISign<TSignKey>> IgnorableSigns { get; set; }
        IEnumerable<IWatchman<TWatchmanKey>> Watchmen { get; set; }

        void Attach(IWatchman<TWatchmanKey> watchman);
        void Detach(IWatchman<TWatchmanKey> watchman);
        void Notify();
    }
}
