using System;
using System.Collections.Generic;
using WIS.Models;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class SignupValidatedPage : ContentPage
    {
        public SignupValidatedPage(PRESIGNUPREQUEST signuprequest)
        {
            InitializeComponent();
            this.BindingContext = new SignupValidatedPageViewModel(signuprequest);
        }
    }
}
