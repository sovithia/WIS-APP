using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Syncfusion.SfSchedule.XForms;
using WIS.Extensions;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class TeacherScheduleViewModel : INotifyPropertyChanged
    {
        Dictionary<DayOfWeek, string> daysOfWeek;
        private List<string> days;
        public TeacherScheduleViewModel()
        {
          
            days = new List<string>() { "1", "2", "3", "4", "5", "6", "7" };
            daysOfWeek = new Dictionary<DayOfWeek, string>() {
                { DayOfWeek.Monday , "1" },
                { DayOfWeek.Tuesday, "2" },
                { DayOfWeek.Wednesday, "3" },
                { DayOfWeek.Thursday, "4" },
                { DayOfWeek.Friday, "5" },
                { DayOfWeek.Saturday, "6" },
                { DayOfWeek.Sunday, "7" }
            };
            ObservableCollection<SFSCHEDULEDATA> tmp = new ObservableCollection<SFSCHEDULEDATA>();
            DataService.Instance.GetTeacherSchedule((schedule) =>
            {            
                var firstday = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
                firstday = firstday.Date;
                DateTime theDay = firstday;
                for (int i = 0; i < 5; i++)
                {
                    List<TeacherScheduleCourse> oneday = schedule.schedulesessionList.Where(line => line.day == days[i]).ToList();
                    foreach (TeacherScheduleCourse sline in oneday)
                    {
                        SFSCHEDULEDATA data = sline.toSFDATA(theDay);
                        tmp.Add(data);

                    }
                    theDay = theDay.AddDays(1);
                }
                this.Courses = tmp;
                this.RaiseOnPropertyChanged("Courses");

            });           
        }

        /// <summary>
        /// collecions for meetings.
        /// </summary>
        public ObservableCollection<SFSCHEDULEDATA> Courses {get;set;}
        /*
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
        */


        /// <summary>
        /// color collection.
        /// </summary>
        //private List<Color> colorCollection;
        /// <summary>
        /// current day meeting.
        /// </summary>
        //private List<string> currentDayMeetings;     
        //private Dictionary<DayOfWeek, int> mondayDistance;


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
