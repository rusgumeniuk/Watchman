using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public class WatchmanPatient<TWatchman, TPatient>
        where TWatchman : IEquatable<TWatchman>
        where TPatient : IEquatable<TPatient>
    {
        public TWatchman WatchmanId { get; set; }
        public WatchmanProfile<TWatchman> Watchman { get; set; }

        public TPatient PatientId { get; set; }
        public Patient<TPatient> Patient { get; set; }
    }
}
