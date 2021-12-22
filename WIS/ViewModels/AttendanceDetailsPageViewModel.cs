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
            DataService.Instance.GetAttendanceDetails(absenceID, (absence) =>
             {
                 Absence = absence;
             });
        }        

   
        public Command SubmitCommand { get; set; }
     
    }
}
