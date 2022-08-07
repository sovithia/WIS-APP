using System;
using System.Collections.ObjectModel;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class InvoiceDetailsToValidateViewModel : BaseViewModel
    {
        public InvoiceDetailsToValidateViewModel(string _invoiceID)
        {
            InvoiceID = _invoiceID;
            ValidateCommand  = new Command(ValidateClicked);
            InvoiceLines = new ObservableCollection<InvoiceElement>();
            DataService.Instance.GetInvoiceDetails(invoiceID, (invoice) =>
            {
                float amt = 0;
                foreach (InvoiceElement line in invoice.invoicefeeList)
                {
                    amt += line.amount;
                    InvoiceLines.Add(line);
                }
                Total = amt;
            });
        }
        #region properties

        public ObservableCollection<InvoiceElement> InvoiceLines { get; set; }

        private float total;
        public float Total
        {
            get
            {
                return total;
            }
            set
            {
                this.SetProperty(ref this.total, value);
            }
        }

        public string invoiceID;
        public string InvoiceID
        {
            get
            {
                return "Receipt No:" + invoiceID;
            }
            set
            {
                this.SetProperty(ref this.invoiceID, value);
            }
        }

        public bool IsParent { get; set; }
        public bool IsRegistrar
        {
            get
            {
                return !IsParent;
            }
        }

        #endregion

        public Command ValidateCommand { get; set; }


        private void ValidateClicked(object obj)
        {
            //SignUpPage page = new SignUpPage();
            //Application.Current.MainPage.Navigation.PushModalAsync(page);
            // Do Something
            DataService.Instance.InvoiceValidatePayment((response) =>
            {
                
            },invoiceID);
        }
    }

}

