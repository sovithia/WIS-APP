﻿using System;
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
        public Invoice currentInvoice { get; set; }

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
       
        public InvoiceDetailsPageViewModel(string _invoiceID)
        {
            InvoiceID = _invoiceID;
            InvoiceLines = new ObservableCollection<InvoiceElement>();
            DataService.Instance.GetInvoiceDetails(invoiceID, (invoice) =>
            {
                currentInvoice = invoice;
                float amt = 0;
                foreach (InvoiceElement line in invoice.invoicefeeList)
                {
                    amt += line.amount;
                    InvoiceLines.Add(line);
                }
                Total = amt;
            });

            this.ACLEDACommand = new Command(this.ACLEDAClicked);
            this.ABACommand = new Command(this.ABAClicked);
            this.CreditCardCommand = new Command(this.CreditCardClicked);
        }

        #region Command
        
        public Command ACLEDACommand { get; set; }
        
        public Command ABACommand { get; set; }
        
        public Command CreditCardCommand { get; set; }

        #endregion

        private async void ACLEDAClicked(object obj)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("ABA Pay", "Pay in ACLEDA App ?", "Yes", "No");
            if (answer)
            {
                
            }               
        }

        private  void CreditCardClicked(Object obj)
        {
            ModalCreditCardPaymentPage page = new ModalCreditCardPaymentPage("10", "firstname", "lastname", "0964222816", currentInvoice);
            Shell.Current.Navigation.PushModalAsync(page);

            
        }

        private async void ABAClicked(object obj)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("ABA Pay", "Pay in ABA App ?", "Yes", "No");
            if (answer)
            {
                DataService.Instance.RequestABAPayment((response) =>
                {
                    string fallbackuri = null;
                    if (Device.RuntimePlatform == Device.iOS)
                        fallbackuri = response.app_store;
                    else if (Device.RuntimePlatform == Device.Android)
                        fallbackuri = response.play_store;
                    Device.BeginInvokeOnMainThread(async () =>{

                        var supportsUri = await Launcher.CanOpenAsync(response.abapay_deeplink);
                        if (supportsUri)
                            await Launcher.OpenAsync(response.abapay_deeplink);                                                    
                        else                        
                            await Launcher.OpenAsync(fallbackuri);                                                
                    });
                    
                },"10","firstname","lastname","0964222816",currentInvoice);
            }

        }

        public void OnAppearing()
        {
            //this.currentInvoice;
            DataService.Instance.ABATransactionCheck((status) =>
            {
                // Not created yet
                if (status.status == "6")
                {
                    // Do nothing
                }
                else if (status.status == "0")
                {
                    // Display Current payment info, hide payment button
                }
            }, this.currentInvoice);
        }

        // ALL THIS PART IS NOT USED FOR THE MOMENT
        // IT's THE CODE TO MANAGE PAYMENT PPROF UPLOAD 
        /*
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
