using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Syncfusion.XForms.SignaturePad;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class AttendanceDetailsPageViewModel : BaseViewModel
    {

        public ABSENCE absence;
        public ABSENCE Absence{
            get{
                return absence;
            }
            set{                
                this.SetProperty(ref this.absence, value);
            }
        }

       
        public bool isSubmitable;
        public bool IsSubmitable {
            get {
                return isSubmitable;
            }
            set
            {
                this.SetProperty(ref this.isSubmitable, value);
            }
        }
                

        public AttendanceDetailsPageViewModel(string absenceID)
        {
            SubmitCommand = new Command(submitClick);            
            DataService.Instance.GetAttendanceDetails(absenceID, (absence) =>
             {
                 Absence = absence;
             });
        }        

   
        public Command SubmitCommand { get; set; }

        private void submitClick(object obj)
        {
            Analytics.TrackEvent(this.GetType().ToString() + " ItemSelected");
            DataService.Instance.ValidateAttendance(Absence.ID,  (success) =>
            {
                if (success)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await AppShell.Current.DisplayAlert("Success", "Attendance validated", "OK");
                        await AppShell.Current.Navigation.PopAsync();
                    });
                    
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await AppShell.Current.DisplayAlert("Error", "Error validating attendance", "OK");
                    });
                }
            });
        }
    }
}
