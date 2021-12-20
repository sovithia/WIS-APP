using System;
namespace WIS.Models
{
    public class Attendance
    {
        public Attendance(){}

        public string start { get; set; }
        public string end { get; set; }
        

        // full
        public string student { get; set; }
        public string teacher { get; set; }
        public string coursename { get; set; }
    }
}
