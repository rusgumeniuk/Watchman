using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public class ControlRequest : IIdentifiedEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid WatchmanId { get; set; }
        public Guid PatientId { get; set; }
        public ControlRequestStatus Status { get; set; } = ControlRequestStatus.Wait;
        public DateTime ConsiderationTime { get; set; }
    }

    public enum ControlRequestStatus : byte
    {
        Wait = 0,
        Refused = 1,
        Accepted = 2
    }
}