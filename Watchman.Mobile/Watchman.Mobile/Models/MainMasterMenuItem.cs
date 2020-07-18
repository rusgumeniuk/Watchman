using System;

namespace Watchman.Mobile.Models
{
    public class MainMasterMenuItem
    {
        public MainMasterMenuItem()
        {
            TargetType = typeof(MainMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
