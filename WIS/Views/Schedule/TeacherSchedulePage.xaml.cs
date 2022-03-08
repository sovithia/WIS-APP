﻿using System;
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
                schedule.WeekViewSettings = new Syncfusion.SfSchedule.XForms.WeekViewSettings()
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
            }

            isWeekView = !isWeekView;
        }
    }
}
