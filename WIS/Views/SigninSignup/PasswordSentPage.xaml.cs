using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WIS.Views
{
    public partial class PasswordSentPage : ContentPage
    {
        public PasswordSentPage()
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
