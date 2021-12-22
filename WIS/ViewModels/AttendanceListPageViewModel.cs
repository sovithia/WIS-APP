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
            
            ItemSelectedCommand = new Command(ItemSelected);
        }

        public void OnAppearing()
        {
            Absences.Clear();
            DataService.Instance.GetAttendance((absences) =>
            {
                foreach (ABSENCE absence in absences)
                {
                    Absences.Add(absence);
                }
            });
        }


        public Command ItemSelectedCommand { get; set; }

        private void ItemSelected(object selectedItem)
        {
            Analytics.TrackEvent(this.GetType().ToString() + " ItemSelected");
            ABSENCE absence = (ABSENCE)((Syncfusion.ListView.XForms.ItemTappedEventArgs)selectedItem).ItemData;             
            Shell.Current.GoToAsync($"AttendanceDetails?ID={absence.id}&IsSubmitable=true");                        
        }
    }
}
