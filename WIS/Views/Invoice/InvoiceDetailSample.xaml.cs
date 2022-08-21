using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using WIS.Models;
using WIS.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class InvoiceDetailSample : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<InvoiceElement> InvoiceLines { get; set; }
        public Invoice currentInvoice { get; set; }
        public Command ACLEDACommand { get; set; }
        public Command ABACommand { get; set; }
        public Command CreditCardCommand { get; set; }

        private float total;
        public float Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;                     
                //this.SetProperty(ref this.total, value);
            }
        }

        public Invoice generateSampleInvoice()
        {
            Invoice inv = new Invoice();
            
            inv.date = new DateTime();
            inv.date_due = inv.date;

            inv.doc = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            inv.studentdisplayname = "Student 1";
            inv.is_paid = "1";
            inv.invoicefeeList = new List<InvoiceElement>()
            {
                new InvoiceElement(){ amount = 100,price = 10,productname = "Book 1",quantity = 10 },
                new InvoiceElement(){ amount = 200,price = 20,productname = "Book 2",quantity = 10 },
            };

            return inv;
        }

        public InvoiceDetailSample()
        {
            InitializeComponent();
            this.BindingContext = this;

            InvoiceLines = new ObservableCollection<InvoiceElement>();
            currentInvoice = generateSampleInvoice();
                        
            float amt = 0;
            foreach (InvoiceElement line in currentInvoice.invoicefeeList)
            {
                amt += line.amount;
                InvoiceLines.Add(line);
            }
            Total = amt;
            

            this.ACLEDACommand = new Command(this.ACLEDAClicked);
            this.ABACommand = new Command(this.ABAClicked);
            this.CreditCardCommand = new Command(this.CreditCardClicked);

            ShowPayment = true;
            ShowCreditcard = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.popUpLayout.Show(0, 600);
        }



        private async void ACLEDAClicked(object obj)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("ABA Pay", "Pay in ACLEDA App ?", "Yes", "No");
            if (answer)
            {

            }
        }

        public bool ShowCreditcard { get; set; }
        public bool ShowPayment { get; set; }

        public Uri URI { get; set; }

        private void CreditCardClicked(Object obj)
        {
            
            ShowCreditcard = true;
            ShowPayment = false;
            
            DataService.Instance.RequestCreditCardPayment((url) =>
            {               
                Device.BeginInvokeOnMainThread(() =>
                {
                    URI = new Uri(url);
                    OnPropertyChanged("URI");
                    OnPropertyChanged("ShowCreditcard");
                    OnPropertyChanged("ShowPayment");
                });
            }, "10", "firstname", "lastname", "0964222816", currentInvoice);
            
            //ModalCreditCardPaymentPage page = new ModalCreditCardPaymentPage("10", "firstname", "lastname", "0964222816", currentInvoice);
            //this.Navigation.PushModalAsync(page);
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
                    Device.BeginInvokeOnMainThread(async () => {

                        var supportsUri = await Launcher.CanOpenAsync(response.abapay_deeplink);
                        if (supportsUri)
                            await Launcher.OpenAsync(response.abapay_deeplink);
                        else
                            await Launcher.OpenAsync(fallbackuri);
                    });

                }, "10", "firstname", "lastname", "0964222816", currentInvoice);
            }

        }
    }
}

