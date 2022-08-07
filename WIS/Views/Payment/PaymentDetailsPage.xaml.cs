using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    [QueryProperty(nameof(ID), "ID")]
    public partial class PaymentDetailsPage : ContentPage
    {
        PaymentDetailsViewModel vm;
        public string ID
        {
            set
            {
                vm = new PaymentDetailsViewModel(value);
                this.BindingContext = vm;
            }
        }

        public PaymentDetailsPage()
        {
            InitializeComponent();
        }
    }
}

