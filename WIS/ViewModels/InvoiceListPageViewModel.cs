using System;
using System.Collections.ObjectModel;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using WIS.Views;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class InvoiceListPageViewModel : BaseViewModel
    {

        public ObservableCollection<Invoice> Invoices { get; set; }


        public Command ItemSelectedCommand { get; set; }
        

        public InvoiceListPageViewModel()
        {
            Invoices = new ObservableCollection<Invoice>();            
            ItemSelectedCommand = new Command(ItemSelected);
        }

        
        public void OnAppearing()
        {
            Invoices.Clear();
            DataService.Instance.GetInvoiceList((invoices) =>
            {
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
            Shell.Current.GoToAsync($"InvoiceDetails?ID={invoice.id}&IsSubmitable=true");                        
        }
    }
}
