using Newtonsoft.Json;

using System;
using System.Runtime.Serialization;

namespace Watchman.BusinessLogic.Models.Users
{
    public class WatchmanPatient<TWatchman, TPatient>
        where TWatchman : IEquatable<TWatchman>
        where TPatient : IEquatable<TPatient>
    {
        public virtual TWatchman WatchmanId { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public WatchmanProfile<TWatchman> Watchman { get; set; }

        public virtual TPatient PatientId { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public Patient<TPatient> Patient { get; set; }
    }
}
