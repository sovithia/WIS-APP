using System;
using System.Collections.Generic;
using Xamarin.Forms;

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
    
    public class SCHEDULELINE
    {
        public string coursename { get; set; }
        public string teachername { get; set; }
        public string classname { get; set; }
        public string starthour { get; set; }
        public string endhour { get; set; }
        public string day { get; set; }

        public SFSCHEDULEDATA toSFDATA(DateTime day)
        {
            SFSCHEDULEDATA data = new SFSCHEDULEDATA();

            int hstart = int.Parse(starthour.Split(':')[0]);
            int mstart = int.Parse(starthour.Split(':')[1]);
            int hend = int.Parse(endhour.Split(':')[0]);
            int mend = int.Parse(endhour.Split(':')[1]);

            data.From = day.AddHours(hstart).AddMinutes(mstart);
            data.To = day.AddHours(hend).AddMinutes(mstart);
            data.EventName = coursename + "\n" + starthour + "-" + endhour + "\n"  + teachername + "\n" + classname;

            data.Color = Color.FromHex("1746A0");
            return data;
        }
    }   

    public class SCHEDULE
    {
        public List<SCHEDULELINE> courses { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string seasonname { get; set; }
        
        public DateTime dStartdate { get { return App.TimestampToDate(int.Parse(startdate)); }}
        public DateTime dEndDate { get { return App.TimestampToDate(int.Parse(enddate)); }}
    }
}
