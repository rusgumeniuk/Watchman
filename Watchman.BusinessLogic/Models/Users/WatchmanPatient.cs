using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public abstract class WatchmanPatient<TWatchman, TPatient>
        where TWatchman : IEquatable<TWatchman>
        where TPatient : IEquatable<TPatient>
    {

        public virtual TWatchman WatchmanId { get; set; }
        public WatchmanProfile<TWatchman> Watchman { get; set; }

        public virtual TPatient PatientId { get; set; }
        public Patient<TPatient> Patient { get; set; }
    }
}
