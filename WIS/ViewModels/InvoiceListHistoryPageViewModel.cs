using System;
using System.Collections.ObjectModel;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class InvoiceListHistoryPageViewModel : BaseViewModel
    {

        public ObservableCollection<INVOICE> Invoices { get; set; }


        public Command ItemSelectedCommand { get; set; }


        public InvoiceListHistoryPageViewModel()
        {
            Invoices = new ObservableCollection<INVOICE>();
            ItemSelectedCommand = new Command(ItemSelected);
        }


        public void OnAppearing()
        {
            Invoices.Clear();
            DataService.Instance.GetInvoiceHistory((invoices) =>
            {
                foreach (INVOICE invoice in invoices)
                {
                    Invoices.Add(invoice);
                }
            });
        }



        private void ItemSelected(object selectedItem)
        {
            Analytics.TrackEvent(this.GetType().ToString() + " ItemSelected");
            INVOICE invoice = (INVOICE)((Syncfusion.ListView.XForms.ItemTappedEventArgs)selectedItem).ItemData;
            Shell.Current.GoToAsync($"InvoiceDetails?ID={invoice.id}");
        }
    }
}
