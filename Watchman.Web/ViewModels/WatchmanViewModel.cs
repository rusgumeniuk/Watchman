using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Users;
using Watchman.Web.Models;

namespace Watchman.Web.ViewModels
{
    public class WatchmanViewModel
    {
        public Guid WatchmanId { get; set; }
        public IEnumerable<PatientProfileAndPersonalInfoPair> PatientsAndPersonalInfoPairs { get; set; }
    }

    public class PatientProfileAndPersonalInfoPair
    {
        public Patient<Guid> Patient { get; set; }
        public PersonalInfo PersonalInformation { get; set; }
    }
}
