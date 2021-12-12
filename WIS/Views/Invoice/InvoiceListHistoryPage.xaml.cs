using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class InvoiceListHistoryPage : ContentPage
    {
        public InvoiceListHistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InvoiceListHistoryPageViewModel vm = (InvoiceListHistoryPageViewModel)this.BindingContext;
            vm.OnAppearing();
        }
    }
}
