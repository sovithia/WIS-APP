using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class InvoiceListHistoryPageViewModel : BaseViewModel
    {

        public ObservableCollection<Invoice> Invoices { get; set; }


        public Command ItemSelectedCommand { get; set; }


        public InvoiceListHistoryPageViewModel()
        {
            Invoices = new ObservableCollection<Invoice>();
            ItemSelectedCommand = new Command(ItemSelected);
            DataService.Instance.GetInvoiceHistory((invoices) =>
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
            Shell.Current.GoToAsync($"InvoiceDetailsPaid?ID={invoice.id}");
        }
    }
}
