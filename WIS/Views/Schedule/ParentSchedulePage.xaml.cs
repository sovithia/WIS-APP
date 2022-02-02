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


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ParentScheduleViewModel vm = (ParentScheduleViewModel)this.BindingContext;
            vm.OnAppearing(() =>{
                if (ChildrenPicker.ItemsSource.Count > 0)
                    ChildrenPicker.SelectedIndex = 0;
            });            
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
