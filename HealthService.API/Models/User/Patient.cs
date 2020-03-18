using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.User
{
    public class Patient : IPatient
    {
        public Guid Id { get; set; }
        public IEnumerable<IHealthMeasurement<Guid, Guid>> HealthMeasurements { get; set; }
        public IPatientActivityState<Guid> CurrentActivityState { get; set; }
        public IPatientHealthState<Guid> CurrentHealthState { get; set; }
        public ISet<ISign<Guid>> IgnorableSigns { get; set; }
        public IEnumerable<IWatchman<Guid>> Watchmen { get; set; }

        public void Attach(IWatchman<Guid> watchman)
        {
            throw new NotImplementedException();
        }
        public void Detach(IWatchman<Guid> watchman)
        {
            throw new NotImplementedException();
        }
        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
