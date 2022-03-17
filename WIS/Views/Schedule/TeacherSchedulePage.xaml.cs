using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class TeacherSchedulePage : ContentPage
    {
        public TeacherSchedulePage()
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
            }            
            else
            {
                switchBtn.Text = "Day View";
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WorkWeekView;                
            }

            isWeekView = !isWeekView;
        }
    }
}
