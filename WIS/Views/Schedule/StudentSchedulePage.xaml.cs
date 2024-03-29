﻿using System;
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
            if (isWeekView == true) // Change to DayView
            {
                switchBtn.Text = "Week View";                                                
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.DayView;

            }
            else // Change to Week View
            {
                switchBtn.Text = "Day View";                
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WorkWeekView;
            }

            isWeekView = !isWeekView;
        }

       
    }
}
