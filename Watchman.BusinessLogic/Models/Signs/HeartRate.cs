using System;

namespace Watchman.BusinessLogic.Models.Signs
{
    public class HeartRate : ISign<Guid>
    {
        public Guid Id { get; set; }
        public ushort Value { get; }

        public HeartRate(Guid id, ushort value)
        {
            Id = id;
            Value = value;
        }
    }
}