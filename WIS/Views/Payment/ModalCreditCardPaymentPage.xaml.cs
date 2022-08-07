using System;
using System.Collections.Generic;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class ModalCreditCardPaymentPage : ContentPage
    {
        public ModalCreditCardPaymentPage()
        {
            InitializeComponent();
        }

        private string amount;
        private string firstname;
        private string lastname;
        private string phone;
        private Invoice invoice;

        public ModalCreditCardPaymentPage(string _amount, string _firstname, string _lastname, string _phone, Invoice _invoice)
        {
            InitializeComponent();
            amount = _amount;
            firstname = _firstname;
            lastname = _lastname;
            phone = _phone;
            invoice = _invoice;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loader.IsVisible = true;
            DataService.Instance.RequestCreditCardPayment((response) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    loader.IsVisible = false;
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = response;
                    webview.Source = htmlSource;
                    //webview.Source = "http://www.google.com";
                });
            }, amount, firstname, lastname, phone, invoice);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}

