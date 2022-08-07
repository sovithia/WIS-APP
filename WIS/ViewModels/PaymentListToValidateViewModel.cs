using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class PaymentListToValidateViewModel : BaseViewModel
    {

        public ObservableCollection<Invoice> Invoices { get; set; }

        public Command ItemSelectedCommand { get; set; }

        public PaymentListToValidateViewModel()
        {
          
            ItemSelectedCommand = new Command(ItemSelected);

            Invoices = new ObservableCollection<Invoice>();
            DataService.Instance.PaymentListToValidate((invoices) =>
            {
                invoices = invoices.OrderByDescending(i => i.date).ToList();
                foreach (Invoice invoice in invoices)
                {
                    Invoices.Add(invoice);
                }
            });

        }

  

        private void ItemSelected(object selectedItem)
        {
            Analytics.TrackEvent(this.GetType().ToString() + " ItemSelected");
            Invoice invoice = (Invoice)((Syncfusion.ListView.XForms.ItemTappedEventArgs)selectedItem).ItemData;

            Shell.Current.GoToAsync($"PaymentABADetails?ID={invoice.id}");
        }
        
    }
}


