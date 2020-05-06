using Newtonsoft.Json;

using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Users
{
    public class PatientSign : PatientSign<Guid>
    { }
    public class PatientSign<TKey> : PatientSign<TKey, ushort>
        where TKey : IEquatable<TKey>
    { }

    public class PatientSign<TKey, TValue>
        where TKey : IEquatable<TKey>
        where TValue : struct
    {
        public string SignType { get; set; }

        public virtual TKey PatientId { get; set; }
        [JsonIgnore]
        public Patient<TKey> Patient { get; set; }

        public PatientSign() { }
        public PatientSign(Patient<TKey> patient, Sign<TKey, TValue> type)
        {
            this.Patient = patient;
            this.SignType = type.GetType().ToString();
        }
    }
}
