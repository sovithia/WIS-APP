using System;
namespace WIS.Models.SAMS
{
    public class SCHEDULE_LAYOUT_TIME
    {
        public SCHEDULE_LAYOUT_TIME(){}

        public DateTime start_time { get; set; }
        public int minute { get; set; }
        public bool is_morning { get; set; }
    }
}
