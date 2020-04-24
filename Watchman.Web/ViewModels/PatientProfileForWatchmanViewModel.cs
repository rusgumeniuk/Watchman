using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Users;
using Watchman.Web.Models;

namespace Watchman.Web.ViewModels
{
    public class PatientProfileForWatchmanViewModel
    {
        public Guid PatientId { get; set; }
        public IEnumerable<PatientSign<Guid, ushort>> IgnorableSigns { get; set; }
        public IEnumerable<IAnalysisResult> AnalysisResults { get; set; }
        public PersonalInfo PatientPersonalInfo { get; set; }
    }
}
