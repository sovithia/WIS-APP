using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Models
{

    public class StudentScheduleCourse
    {
        
        public string teacherdisplayname { get; set; } // schedule_session staff_id External API OK
        public string roomname { get; set; } // schedule_data classroom // self assigned OK
        public string coursename { get; set; } // schedule_session subject OK
        public string starttime { get; set; } // schedule_layout_times starttime 
        public int minute { get; set; } // schedule_layout_times  minutes        
        public string day { get; set; }  // schedule_session day  MON,TUE,WED etc.. -> 1,2,3


        public SFSCHEDULEDATA toSFDATA(DateTime theday)
        {            

            SFSCHEDULEDATA data = new SFSCHEDULEDATA();
            int hstart = int.Parse(starttime.Split(':')[0]);
            int mstart = int.Parse(starttime.Split(':')[1]);           
            data.From = theday.AddHours(hstart).AddMinutes(mstart);            
            data.To = data.From.AddMinutes(minute);

            string fromHours = data.From.Hour.ToString() + ":" + data.From.Minute.ToString();            
            string toHours = data.To.Hour.ToString() + ":" + data.To.Minute.ToString();

            data.EventName = coursename + "\n" + fromHours + "\n" + toHours + "\n" + teacherdisplayname + "\nRoom:" + roomname;
            data.Color = Color.FromHex("1746A0");
            
            return data;
        }
    }

    public class StudentSchedule
    {
        // student_data_record -> grade_id
        public List<StudentScheduleCourse> schedulesessionList { get; set; }
        public int starttimestamp { get; set; } // Schedule_data start
        public int endtimestamp { get; set; } // Schedule_data end
        public string seasonname { get; set; } //Study_semester name

        public DateTime dStartdate { get { return App.TimestampToDate(starttimestamp); } }
        public DateTime dEndDate { get { return App.TimestampToDate(endtimestamp); } }
    }
   
}
