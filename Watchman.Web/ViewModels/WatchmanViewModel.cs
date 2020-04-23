using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.Web.ViewModels
{
    public class WatchmanViewModel
    {
        public Guid WatchmanId { get; set; }
        public IEnumerable<Patient<Guid>> Patients { get; set; }
    }
}
