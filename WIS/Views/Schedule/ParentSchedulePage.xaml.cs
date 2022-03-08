using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class ParentSchedulePage : ContentPage
    {
        public ParentSchedulePage()
        {
            InitializeComponent();
        }


        void Picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Picker picker = (Picker)sender;
            ParentScheduleViewModel vm = (ParentScheduleViewModel)this.BindingContext;
            vm.SelectChildren(picker.SelectedIndex);            
        }

        bool isWeekView;
        void switchView(System.Object sender, System.EventArgs e)
        {
            if (isWeekView == true)
            {
                switchBtn.Text = "Week View";
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.DayView;
                schedule.DayViewSettings = new Syncfusion.SfSchedule.XForms.DayViewSettings()
                {
                    StartHour = 7,
                    EndHour = 19,
                    WorkStartHour = 7,
                    WorkEndHour = 19,
                };
            }

            else
            {
                switchBtn.Text = "Day View";
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WorkWeekView;
                schedule.WeekViewSettings = new Syncfusion.SfSchedule.XForms.WeekViewSettings()
                {
                    StartHour = 7,
                    EndHour = 19,
                    WorkStartHour = 7,
                    WorkEndHour = 19,
                };
            }

            isWeekView = !isWeekView;
        }



    }
}
