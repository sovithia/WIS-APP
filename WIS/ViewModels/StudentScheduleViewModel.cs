using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Syncfusion.SfSchedule.XForms;
using WIS.Extensions;
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
        private List<string> days;
        
        public StudentScheduleViewModel()
        {
            
            days = new List<string>(){         
                "1","2","3","4","5","6","7"
            };

            ObservableCollection<SFSCHEDULEDATA> tmp = new ObservableCollection<SFSCHEDULEDATA>();            
            DataService.Instance.GetStudentSchedule((schedule) =>
            {
                if (schedule.schedulesessionList.Count > 0)
                {
                    GradeName = schedule.schedulesessionList[0].gradename + "-" + schedule.schedulesessionList[0].roomname;

                }
                    
                var firstday = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
                firstday = firstday.Date;
                DateTime theDay = firstday;
                if (schedule.schedulesessionList != null)
                {
                    List<DateTime> times = new List<DateTime>();
                    for (int i = 0; i < 5; i++)
                    {
                        List<StudentScheduleCourse> oneday = schedule.schedulesessionList.Where(line => line.day == days[i]).ToList();
                        foreach (StudentScheduleCourse sline in oneday)
                        {
                            SFSCHEDULEDATA data = sline.toSFDATA(theDay);
                            if (times.Contains(data.From)) // Dirty fix to contain double data
                                continue;
                            tmp.Add(data);
                            times.Add(data.From);
                        }
                        theDay = theDay.AddDays(1);
                    }
                    this.Courses = tmp;
                    this.RaiseOnPropertyChanged("Courses");
                    this.RaiseOnPropertyChanged("GradeName");
                }
            });
            
        }


        /// <summary>
        /// collecions for meetings.
        /// </summary>
        //private ObservableCollection<SFSCHEDULEDATA> courses;
        public ObservableCollection<SFSCHEDULEDATA> Courses { get; set; }

        public string GradeName { get; set; }

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