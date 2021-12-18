using System;
namespace WIS.Models.SAMS
{
    public class SCHEDULE_LAYOUT_DATA
    {
        public SCHEDULE_LAYOUT_DATA(){}

        public string name { get; set; }
        public string days { get; set; }
        public string detail { get; set; }
        public string note { get; set; }
        public DateTime afternoon_time { get; set; }
        public string layout_type { get; set; }

    }
}
