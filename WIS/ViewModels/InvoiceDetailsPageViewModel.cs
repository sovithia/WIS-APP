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


        SfSignaturePad signaturePad;


        private Invoice currentInvoice;


        private ImageSource proofImageIn;
        public ImageSource ProofImageIn
        {
            get{
                return proofImageIn;  
            }
            set{
                this.SetProperty(ref this.proofImageIn, value);
            }
            
        }

        private ImageSource parentsigImageIn;
        public ImageSource ParentSigImageIn{
            get{
                return parentsigImageIn;
            }
            set{
                this.SetProperty(ref this.parentsigImageIn, value);
            }
        }

        private ImageSource registrarsigImageIn;
        public ImageSource RegistrarSigImageIn
        {
            get{
                return registrarsigImageIn;
            }
            set{
                this.SetProperty(ref this.registrarsigImageIn, value);
            }
        }

      

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
                
            float amt = 0;
            DataService.Instance.GetInvoiceDetails(_invoiceID, (invoice) =>
             {
                

                 ObservableCollection<InvoiceElement> tmp = new ObservableCollection<InvoiceElement>();
                 foreach (InvoiceElement line in invoice.invoicefeeList)
                 {
                     amt += float.Parse(line.amount);
                     tmp.Add(line);
                 }
                 Total = "$ " + amt.ToString();    
                 InvoiceLines = tmp;
                 
             });
                     
        }
   
        


    }
}
