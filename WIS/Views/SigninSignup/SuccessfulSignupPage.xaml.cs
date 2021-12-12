using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WIS.Views
{
    public partial class SuccessfulSignupPage : ContentPage
    {
        public SuccessfulSignupPage()
        {
            InitializeComponent();
        }

        void loginClicked(System.Object sender, System.EventArgs e)
        {
            var page = new LoginPage();
            Application.Current.MainPage = page;
        }

        
    }
}
