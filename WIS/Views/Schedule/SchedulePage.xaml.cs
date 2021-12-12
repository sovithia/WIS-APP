using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WIS.Views
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
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
                     
                Schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.DayView;
                Schedule.WorkWeekViewSettings = new Syncfusion.SfSchedule.XForms.WorkWeekViewSettings()
                {
                    StartHour = 7,
                    EndHour = 17
                };
            }

            else
            {
                switchBtn.Text = "Day View";
                Schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WorkWeekView;
                Schedule.WorkWeekViewSettings = new Syncfusion.SfSchedule.XForms.WorkWeekViewSettings()
                {
                    StartHour = 7,
                    EndHour = 17
                };
            }
                
            isWeekView = !isWeekView;
        }
    }
}
