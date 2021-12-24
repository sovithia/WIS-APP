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
    public class SFSCHEDULEDATA
    {
        public string EventName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Color Color { get; set; }
        public string StartTimeZone { get; set; }
        public string EndTimeZone { get; set; }
    }

    
    public class StudentScheduleViewModel : INotifyPropertyChanged
    {
        Dictionary<DayOfWeek, string> daysOfWeek;
        private List<string> days;
        public StudentScheduleViewModel()
        {                      
            days = new List<string>(){         
                "1","2","3","4","5","6","7"
            };
            daysOfWeek = new Dictionary<DayOfWeek, string>() {
                { DayOfWeek.Monday , "1" },
                { DayOfWeek.Tuesday, "2" },
                { DayOfWeek.Wednesday, "3" },
                { DayOfWeek.Thursday, "4" },
                { DayOfWeek.Friday, "5" },
                { DayOfWeek.Saturday, "6" },
                { DayOfWeek.Sunday, "7" }
            };
            VisibleDateChanged = new Command<VisibleDatesChangedEventArgs>(onVisibleDateChanged);
            
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
        
        private Dictionary<DayOfWeek, int> mondayDistance;
        
        public Command VisibleDateChanged { get; set; }

        private void onVisibleDateChanged(VisibleDatesChangedEventArgs args)
        {                              
            DataService.Instance.GetStudentSchedule((schedule) =>
            {                                
                this.courses = new ObservableCollection<SFSCHEDULEDATA>();                
                var firstday = args.visibleDates[0];
                firstday = firstday.Date;                
                if (firstday > schedule.dStartdate && schedule.dEndDate > firstday)
                {
                    if (args.visibleDates.Count == 1) // single day view
                    {
                        string day = daysOfWeek[firstday.DayOfWeek];
                        List<StudentScheduleCourse> oneday = schedule.schedulesessionList.Where(line => line.day == day).ToList();
                        foreach (StudentScheduleCourse sline in oneday)
                        {
                            SFSCHEDULEDATA data = sline.toSFDATA(firstday);
                            this.courses.Add(data);

                        }
                    }
                    else // weekview
                    {
                        DateTime theDay = firstday;
                        for (int i = 0; i < 5; i++)
                        {
                            List<StudentScheduleCourse> oneday = schedule.schedulesessionList.Where(line => line.day == days[i]).ToList();
                            foreach (StudentScheduleCourse sline in oneday)
                            {
                                SFSCHEDULEDATA data = sline.toSFDATA(theDay);
                                this.courses.Add(data);

                            }
                            theDay = theDay.AddDays(1);
                        }
                    }                  
                    this.RaiseOnPropertyChanged("Courses");
                }                
            });
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke method when property changed.
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}