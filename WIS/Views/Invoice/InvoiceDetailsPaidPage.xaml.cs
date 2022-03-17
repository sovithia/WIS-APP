using System;
using System.Collections.Generic;
using System.Globalization;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    [QueryProperty(nameof(ID), "ID")]
    public partial class InvoiceDetailsPaidPage : ContentPage
    {
        public string ID
        {
            set
            {
                this.BindingContext = new InvoiceDetailsPaidPageViewModel(value);
            }
        }

        public InvoiceDetailsPaidPage()
        {
            CultureInfo myCurrency = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;
            InitializeComponent();
        }
    }   
}
