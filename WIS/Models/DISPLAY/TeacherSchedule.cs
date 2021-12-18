using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WIS.Models.DISPLAY
{

    public class TeacherScheduleCourse
    {
        public string coursename { get; set; } // schedule_session subject
        public string classname { get; set; } //  schedule_teacher_session grade
        public string roomname{ get; set; } // schedule_data classroom (Campus name ?)
        public string starthour { get; set; } // schedule_layout_times starttime 
        public string endhour { get; set; } // schedule_layout_times  minutes
        public string day { get; set; }  // schedule_session day  MON,TUE,WED etc.. -> 1,2,3

        public SFSCHEDULEDATA toSFDATA(DateTime theday)
        {
            SFSCHEDULEDATA data = new SFSCHEDULEDATA();

            int hstart = int.Parse(starthour.Split(':')[0]);
            int mstart = int.Parse(starthour.Split(':')[1]);
            int hend = int.Parse(endhour.Split(':')[0]);
            int mend = int.Parse(endhour.Split(':')[1]);

            data.From = theday.AddHours(hstart).AddMinutes(mstart);
            data.To = theday.AddHours(hend).AddMinutes(mstart);
            data.EventName = coursename + "\n" + starthour + "-" + endhour + "\n" + classname + "\n" + classname;

            data.Color = Color.FromHex("1746A0");
            return data;
        }
    }

    public class TeacherSchedule
    {
        public List<TeacherScheduleCourse> courses { get; set; }
        public string startdate { get; set; } // Schedule_data start
        public string enddate { get; set; } // Schedule_data end
        public string seasonname { get; set; } //Study_semester name

        public DateTime dStartdate { get { return App.TimestampToDate(int.Parse(startdate)); } }
        public DateTime dEndDate { get { return App.TimestampToDate(int.Parse(enddate)); } }
    }
}
