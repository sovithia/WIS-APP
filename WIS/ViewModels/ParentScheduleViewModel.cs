﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WIS.Extensions;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class ParentScheduleViewModel
    {
        private ObservableCollection<string> children;
        public ObservableCollection<string> Children {
            get{
                return children;
            }
            set{
                this.children = value;
                this.RaiseOnPropertyChanged("Children");
            }
        }


        public string selectedChildren;
        public string SelectedChildren
        {
            get
            {
                return selectedChildren;
            }
            set
            {
                selectedChildren = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedChildren"));
            }
        }        


        Dictionary<string, ObservableCollection<SFSCHEDULEDATA>> scheduleList;        
        private List<string> days;
        public ParentScheduleViewModel()
        {

            days = new List<string>() { "1", "2", "3", "4", "5", "6", "7" };
            scheduleList = new Dictionary<string, ObservableCollection<SFSCHEDULEDATA>>();
            children = new ObservableCollection<string>();
        }

        /// <summary>
        /// collecions for meetings.
        /// </summary>
        private ObservableCollection<SFSCHEDULEDATA> courses;
        public ObservableCollection<SFSCHEDULEDATA> Courses
        {
            get{
                return courses;
            }
            set{
                this.courses = value;
                this.RaiseOnPropertyChanged("Courses");                
            }
        }


        /// <summary>
        /// color collection.
        /// </summary>
        //private List<Color> colorCollection;
        /// <summary>
        /// current day meeting.
        /// </summary>
        //private List<string> currentDayMeetings;     
        //private Dictionary<DayOfWeek, int> mondayDistance;

        public void OnAppearing(readyDelegate done)
        {
            if (this.courses == null)
            {
                DataService.Instance.GetParentSchedule((schedules) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        foreach (string child in schedules.Keys.ToList())
                        {
                            children.Add(child);
                        }
                        this.RaiseOnPropertyChanged("Children");
                        SelectedChildren = children.ElementAt(0);

                        foreach (KeyValuePair<string, StudentSchedule> schedule in schedules)
                        {

                            var firstday = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
                            firstday = firstday.Date;
                            DateTime theDay = firstday;

                            ObservableCollection<SFSCHEDULEDATA> tmp = new ObservableCollection<SFSCHEDULEDATA>();
                            if (schedule.Value.schedulesessionList != null)
                            {
                                for (int i = 0; i < 5; i++)
                                {

                                    List<StudentScheduleCourse> oneday = schedule.Value.schedulesessionList.Where(line => line.day == days[i]).ToList();
                                    foreach (StudentScheduleCourse sline in oneday)
                                    {
                                        SFSCHEDULEDATA data = sline.toSFDATA(theDay);
                                        tmp.Add(data);
                                    }
                                    theDay = theDay.AddDays(1);
                                }
                            }
                            scheduleList[schedule.Key] = tmp;
                        }
                        done();                        
                    });
                                                                            
                });
           
            }            
        }

        public void SelectChildren(int index)
        {
         
            ObservableCollection<SFSCHEDULEDATA> selectedSchedule =  scheduleList[scheduleList.Keys.ElementAt(index)];
            this.courses = selectedSchedule;
            this.RaiseOnPropertyChanged("Courses");            
        }


        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke method when property changed.
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// 
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}