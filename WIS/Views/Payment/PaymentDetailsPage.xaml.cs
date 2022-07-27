using System;
using System.Collections.Generic;
using WIS.Models;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class PaymentDetailsPage : ContentPage
    {
        public PaymentDetailsPage()
        {
            InitializeComponent();
        }

        public PaymentDetailsPage(ABATransaction transaction)
        {
            InitializeComponent();
            this.BindingContext = new PaymentDetailsViewModel(transaction);
        }

    }
}

