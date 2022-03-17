using System;
using System.Collections.Generic;
using Syncfusion.SfSchedule.XForms;
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
            //schedule.WeekViewSettings = new WeekViewSettings()
            //{
            //    ShowAllDay = false
            //};
            
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
