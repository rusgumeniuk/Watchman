using System;
using System.Collections.Generic;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.User
{
    public class Wathchman : IWatchman
    {
        public Guid Id { get; set; }
        public IEnumerable<IPatient<Guid, Guid, Guid, Guid, Guid, Guid>> Patients { get; set; }

        public void Update<TPatientKey>(TPatientKey patientKey) where TPatientKey : IEquatable<TPatientKey>
        {
            throw new NotImplementedException();
        }
    }
}
