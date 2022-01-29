using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Plugin.Media;
using Syncfusion.XForms.SignaturePad;
using WIS.Interfaces;
using WIS.Models;
using WIS.Services;
using WIS.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class InvoiceDetailsPageViewModel : BaseViewModel
    {

#region properties

        public bool IsSubmitable { get; set; }

        /*
        private ObservableCollection<InvoiceElement> invoiceLines;
        public ObservableCollection<InvoiceElement> InvoiceLines
        {
            get {
                return invoiceLines;
            }
            set
            {
                this.SetProperty(ref this.invoiceLines, value);
            }
        }
        */
        public ObservableCollection<InvoiceElement> InvoiceLines { get; set; }

        private string total;
        public string Total
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
                return "Invoice No:" + invoiceID;
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



        public string AcknowledgmentText { get; set; }
        public InvoiceDetailsPageViewModel(string _invoiceID)
        {
            InvoiceID = _invoiceID;
                        
           
            string type = Preferences.Get("TYPE", "");
            if (type == "PARENT")
            {
                IsParent = true;
                IsSubmitable = true;
                AcknowledgmentText = "I acknowledge, that I have paid and attach the payment proof.";
            }
            else
            {
                IsParent = false;
                AcknowledgmentText = "I verified the payment and validate it.";                
            }                            
            InvoiceLines = new ObservableCollection<InvoiceElement>();
        }



        public void OnAppearing()
        {
            if (InvoiceLines.Count == 0)
            {                
                DataService.Instance.GetInvoiceDetails(invoiceID, (invoice) =>
                {
                        float amt = 0;
                        foreach (InvoiceElement line in invoice.invoicefeeList)
                        {
                            amt += float.Parse(line.amount);
                            InvoiceLines.Add(line);
                        }
                        Total = amt.ToString() + " $";                                        
                });              
            }
        }


        // ALL THIS PART IS NOT USED FOR THE MOMENT
        // IT's THE CODE TO MANAGE PAYMENT PPROF UPLOAD 
        /*
        private Invoice currentInvoice;

        SfSignaturePad signaturePad;
        private ImageSource proofImageIn;
        public ImageSource ProofImageIn
        {
            get
            {
                return proofImageIn;
            }
            set
            {
                this.SetProperty(ref this.proofImageIn, value);
            }

        }

        private ImageSource parentsigImageIn;
        public ImageSource ParentSigImageIn
        {
            get
            {
                return parentsigImageIn;
            }
            set
            {
                this.SetProperty(ref this.parentsigImageIn, value);
            }
        }

        private ImageSource registrarsigImageIn;
        public ImageSource RegistrarSigImageIn
        {
            get
            {
                return registrarsigImageIn;
            }
            set
            {
                this.SetProperty(ref this.registrarsigImageIn, value);
            }
        }
        */


    }
}
