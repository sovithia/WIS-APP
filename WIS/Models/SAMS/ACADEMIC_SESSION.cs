using System;
namespace WIS.Models.SAMS
{
    public class ACADEMIC_SESSION
    {
        public ACADEMIC_SESSION(){}
        public string shortname { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public string note { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public string kh_name { get; set; }
        public string kh_detail { get; set; }

    }
}
