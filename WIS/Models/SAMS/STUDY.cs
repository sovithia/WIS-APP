using System;
namespace WIS.Models.SAMS
{
    public class STUDY
    {
        public STUDY(){}

        public string shortname { get; set; }
        public string name { get; set; }
        public string detail { get; set; }

        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public string kh_name { get; set; }
        public string kh_detail { get; set; }

    }
}
