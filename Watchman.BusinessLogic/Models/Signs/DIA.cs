using System;

namespace Watchman.BusinessLogic.Models.Signs
{
    public class DIA : ISign<Guid>
    {
        public Guid Id { get; set; }
        public ushort Value { get; }

        public DIA(Guid id, ushort value)
        {
            Id = id;
            Value = value;
        }
    }
}