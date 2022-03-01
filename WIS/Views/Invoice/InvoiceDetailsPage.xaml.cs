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

        public string ID
        {
            set{
                this.BindingContext = new InvoiceDetailsPageViewModel(value);                
            }
        }

        public InvoiceDetailsPage(){

            CultureInfo myCurrency = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;
            InitializeComponent();            
        }
      

    }
}
