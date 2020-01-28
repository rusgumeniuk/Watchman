using System;

namespace Watchman.BusinessLogic.Models.Signs
{
    public class SYS : ISign<Guid>
    {
        public Guid Id { get; set; }
        public ushort Value { get; }

        public SYS(Guid id, ushort value)
        {
            Id = id;
            Value = value;
        }
    }
}