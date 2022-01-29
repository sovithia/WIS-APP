using System;
using System.Collections.Generic;
using WIS.Models;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    [QueryProperty(nameof(ID), "ID")]
    public partial class InvoiceDetailsPage : ContentPage
    {

        public string ID
        {
            set
            {
                this.BindingContext = new InvoiceDetailsPageViewModel(value);                
            }
        }

        public InvoiceDetailsPage(){
            
            InitializeComponent();            
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
           // signaturePad.Clear();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InvoiceDetailsPageViewModel vm = (InvoiceDetailsPageViewModel)this.BindingContext;
            vm.OnAppearing();

        }

    }
}
