using System;
using System.Drawing;

namespace WIS.Models
{
    public class SFSCHEDULEDATA
    {
        public string EventName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Color Color { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int NumberOfStudents { get; set; }
        public string StartTimeZone { get { return "W. Europe Standard Time"; } }
        public string EndTimeZone { get { return "W. Europe Standard Time"; } }

    }
    
}
