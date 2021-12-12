using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Syncfusion.SfSchedule.XForms;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class COURSEDATA
    {
        public string day;
        public string startHour;
        public string endHour;
        public string coursename;
        public string teachername;
    }

    public class ScheduleViewModel : BaseViewModel
    {
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
                this.SetProperty(ref this.courses, value);
            }
        }

        /// <summary>
        /// color collection.
        /// </summary>
        private List<Color> colorCollection;

        /// <summary>
        /// current day meeting.
        /// </summary>
        private List<string> currentDayMeetings;

        public DayOfWeek getClosestSchoolDay(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
                return DayOfWeek.Monday;
            else if (day == DayOfWeek.Saturday)
                return DayOfWeek.Monday;
            else
                return day;
        }


        
        private Dictionary<DayOfWeek, int> mondayDistance;
        private List<string> days;

        public Command VisibleDateChanged { get; set; }

        private void onVisibleDateChanged(VisibleDatesChangedEventArgs args)
        {
            //this.courses.Clear();
            
            DataService.Instance.GetSchedule((schedule) =>
            {
                var tmpList = new ObservableCollection<SFSCHEDULEDATA>();

                var firstday = args.visibleDates[0];
                firstday = firstday.AddHours(-5);
                Console.WriteLine("First Visible " + firstday.Day);

                if (firstday > schedule.dStartdate && schedule.dEndDate > firstday)
                {                    
                    DateTime theDay = firstday;
                    for (int i = 0; i < 5; i++)
                    {                                                
                        List<SCHEDULELINE> oneday = schedule.courses.Where(line => line.day == days[i]).ToList();
                        foreach (SCHEDULELINE sline in oneday)
                        {
                             SFSCHEDULEDATA data = sline.toSFDATA(theDay);                            
                             tmpList.Add(data);                          
                        }
                        theDay = theDay.AddDays(1);
                    }
                    this.Courses = tmpList;
                }
            });
        }
       

        public ScheduleViewModel()
        {
            mondayDistance = new Dictionary<DayOfWeek, int>()
            {
                { DayOfWeek.Monday, 0 },
                { DayOfWeek.Tuesday,1 },
                { DayOfWeek.Wednesday,2 },
                { DayOfWeek.Thursday,4 },
                { DayOfWeek.Friday,5 },
                { DayOfWeek.Saturday,6 },
                { DayOfWeek.Sunday,7 },

            };

            this.Courses = new ObservableCollection<SFSCHEDULEDATA>();
            days = new List<string>(){
                "MON","TUE","WED","THU","FRI","SAT","SUN"
            };
            VisibleDateChanged = new Command<VisibleDatesChangedEventArgs>(onVisibleDateChanged);            
        }




        /// <summary>
        /// Gets or sets meetings.
        /// </summary>
        ///
      
        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke method when property changed.
        /// </summary>
        /// <param name="propertyName">property name</param>
        //private void RaiseOnPropertyChanged(string propertyName)
        //{
        //    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}