using System;
using System.Collections.ObjectModel;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using WIS.Views;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class AttendanceListPageViewModel : BaseViewModel
    {        
        public ObservableCollection<ABSENCE> Absences { get; set; }


        public AttendanceListPageViewModel()
        {
            Absences = new ObservableCollection<ABSENCE>();            
            //ItemSelectedCommand = new Command(ItemSelected);
            DataService.Instance.GetAttendance((absences) =>
            {
                foreach (ABSENCE absence in absences)
                {
                    Absences.Add(absence);
                }               
            });
        }

        

        /*
        public void OnAppearing()
        {
            if(Absences.Count == 0)
            {
                DataService.Instance.GetAttendance((absences) =>
                {
                    foreach (ABSENCE absence in absences)
                    {
                        Absences.Add(absence);
                    }
                    IsLoading = false;
                });
            }            
        }
        */
     
    }
}
