﻿using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class PaymentListToValidatePage : ContentPage
    {
        public PaymentListToValidatePage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
           // ((PaymentListToValidateViewModel)this.BindingContext).OnAppearing();
        }
    }
}

