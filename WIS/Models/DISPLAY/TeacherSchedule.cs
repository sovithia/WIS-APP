using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Models
{    
    public class TeacherScheduleCourse
    {
        public string t_gradename { get; set; } // schedule_teacher_session grade
        public string t_coursename { get; set; } // schedule_teacher_session subject        
        public string t_roomname{ get; set; } // schedule_teacher_session classroom (Campus name ?)
        public string t_starttime { get; set; } // schedule_layout_times starttime         
        public int t_minute { get; set; } // schedule_layout_times  minutes        
        public string day { get; set; }  // schedule_session day  MON,TUE,WED etc.. -> 1,2,3

        public SFSCHEDULEDATA toSFDATA(DateTime theday)
        {
            SFSCHEDULEDATA data = new SFSCHEDULEDATA();
            int hstart = int.Parse(t_starttime.Split(':')[0]);
            int mstart = int.Parse(t_starttime.Split(':')[1]);
            data.From = theday.AddHours(hstart).AddMinutes(mstart);
            data.To = data.From.AddMinutes(t_minute);

            string fromHours = data.From.Hour.ToString() + ":" + data.From.Minute.ToString();
            string toHours = data.To.Hour.ToString() + ":" + data.To.Minute.ToString();
            
            data.EventName = t_coursename + "\n" + fromHours + "-" + toHours + "\n" + t_gradename + "\n" + t_roomname;
            data.Color = Color.FromHex("1746A0");            
            return data;
        }
    }

    public class TeacherSchedule
    {
        public List<TeacherScheduleCourse> schedulesessionList { get; set; }
        public int starttimestamp { get; set; } // Schedule_data start
        public int endtimestamp { get; set; } // Schedule_data end
        public string seasonname { get; set; } //Study_semester name

        public DateTime dStartdate { get { return App.TimestampToDate(starttimestamp); } }
        public DateTime dEndDate { get { return App.TimestampToDate(endtimestamp); } }
    }
}
