using System;
using System.Collections.Generic;
using WIS.Models;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class PaymentABADetailsPage : ContentPage
    {
        public PaymentABADetailsPage()
        {
            InitializeComponent();
        }

        public PaymentABADetailsPage(ABATransaction transaction)
        {
            InitializeComponent();
            this.BindingContext = new PaymentABADetailsViewModel(transaction);
        }

    }
}

