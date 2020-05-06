using System;
using System.Collections.Generic;
using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Users;
using Watchman.Web.Models;

namespace Watchman.Web.ViewModels
{
    public class PatientProfileViewModel
    {
        public PatientInfo Patient { get; set; }
        public IEnumerable<PatientSign<Guid>> IgnorableSigns { get; set; }
        public IEnumerable<IAnalysisResult> AnalysisResults { get; set; }

        public IEnumerable<PersonalInfoRequestIdPair> ControlRequests { get; }
        public IEnumerable<WatchmanAndPersonalInfoPair> Watchmen { get; set; }

        public PatientProfileViewModel(Patient<Guid> patient, IEnumerable<PersonalInfoRequestIdPair> pendingRequests)
        {
            this.Patient = patient as PatientInfo;
            this.ControlRequests = pendingRequests;
        }
    }

    public class PersonalInfoRequestIdPair
    {
        public PersonalInfo PersonalInformation { get; }
        public Guid RequestId { get; }

        public PersonalInfoRequestIdPair(PersonalInfo personalInformation, Guid requestId)
        {
            PersonalInformation = personalInformation;
            RequestId = requestId;
        }
    }

    public class WatchmanAndPersonalInfoPair
    {
        public WatchmanProfile<Guid> Watchman { get; set; }
        public PersonalInfo PersonalInformation { get; set; }
    }
}
