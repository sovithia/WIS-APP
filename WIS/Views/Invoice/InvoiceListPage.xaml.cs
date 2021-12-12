using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class InvoiceListPage : ContentPage
    {
        public InvoiceListPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InvoiceListPageViewModel vm = (InvoiceListPageViewModel)this.BindingContext;
            vm.OnAppearing();
        }
        
    }
}
