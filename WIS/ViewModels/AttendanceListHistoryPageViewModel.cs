using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class AttendanceListHistoryPageViewModel : BaseViewModel
    {
        public bool IsLoading { get; set; }
        private bool loadstarted;
        /*
        public ObservableCollection<ABSENCE> absences;
        public ObservableCollection<ABSENCE> Absences {
            get { return absences; }
            set { this.SetProperty(ref this.absences, value); }
        }
        */
        public ObservableCollection<ABSENCE> Absences { get; set; }

        public AttendanceListHistoryPageViewModel()
        {
            Absences = new ObservableCollection<ABSENCE>();            
            IsLoading = true;
            DataService.Instance.GetAttendanceHistory((absences) =>
            {
                foreach (ABSENCE absence in absences)
                {
                    Absences.Add(absence);
                }
                IsLoading = false;               
            });
        }

        /*
        public void OnAppearing()
        {
            if (loadstarted == true)
                return;
            if (Absences.Count == 0)
            {
                loadstarted = true;
                DataService.Instance.GetAttendanceHistory((absences) =>
                {
                    foreach (ABSENCE absence in absences)
                    {
                        Absences.Add(absence);
                    }
                    loadstarted = false;
                });
            }            
        }
        */

    }
}
