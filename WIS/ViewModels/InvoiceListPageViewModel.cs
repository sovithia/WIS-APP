using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            ItemSelectedCommand = new Command(ItemSelected);
            Invoices = new ObservableCollection<Invoice>();
            DataService.Instance.GetInvoiceList((invoices) =>
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
            Shell.Current.GoToAsync($"InvoiceDetails?ID={invoice.id}&IsSubmitable=true");                        
        }

    }
}
