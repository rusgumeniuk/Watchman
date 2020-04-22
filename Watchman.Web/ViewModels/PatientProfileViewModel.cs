using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Users;
using Watchman.Web.Models;

namespace Watchman.Web.ViewModels
{
    public class PatientProfileViewModel
    {
        public PatientInfo Patient { get; }
        public IEnumerable<PersonalInfoRequestIdPair> ControlRequests { get; }

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
}
