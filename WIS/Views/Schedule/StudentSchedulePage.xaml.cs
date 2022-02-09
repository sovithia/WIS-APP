using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class StudentSchedulePage : ContentPage
    {
        public StudentSchedulePage()
        {
            isWeekView = false;
            InitializeComponent();
        }
        
        bool isWeekView;
        void switchView(System.Object sender, System.EventArgs e)
        {
            if (isWeekView == true)
            {
                switchBtn.Text = "Week View";                
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.DayView;
                schedule.WorkWeekViewSettings = new Syncfusion.SfSchedule.XForms.WorkWeekViewSettings()
                {
                    StartHour = 7,
                    EndHour = 17                    
                };
            }

            else
            {
                switchBtn.Text = "Day View";
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WorkWeekView;
                schedule.WorkWeekViewSettings = new Syncfusion.SfSchedule.XForms.WorkWeekViewSettings()
                {
                    StartHour = 7,
                    EndHour = 17
                };
            }

            isWeekView = !isWeekView;
        }

       
    }
}
