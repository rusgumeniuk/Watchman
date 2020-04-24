using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.Web.ViewModels
{
    public class PatientPartialViewModel
    {
        public Guid PatientId { get; set; }
        public IEnumerable<PatientSign<Guid, ushort>> IgnorableSigns { get; set; }
        public IEnumerable<IAnalysisResult> AnalysisResults { get; set; }
    }
}
