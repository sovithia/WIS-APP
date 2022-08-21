using System;
using System.Collections.Generic;
using System.Globalization;
using WIS.Models;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    [QueryProperty(nameof(ID), "ID")]
    public partial class InvoiceDetailsPage : ContentPage
    {

        InvoiceDetailsPageViewModel vm;
        public string ID
        {
            set{
                vm = new InvoiceDetailsPageViewModel(value);
                this.BindingContext = vm;
            }
        }



        public InvoiceDetailsPage(){

            CultureInfo myCurrency = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //vm.OnAppearing();

        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }

    }
}
