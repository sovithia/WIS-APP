using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class PaymentListSearchPage : ContentPage
    {
        public PaymentListSearchPage()
        {
            InitializeComponent();            
        }

        private void DatePickerDateFrom_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateFromEntry.Text = string.Format("{0:dd MMM yyyy}", e.NewValue);
        }

        private void DatePickerDateTo_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateToEntry.Text = string.Format("{0:dd MMM yyyy}", e.NewValue);
        }

        private void DatePickerFrom_Clicked(object sender, System.EventArgs e)
        {
            datePickerFrom.IsOpen = true;
        }

        private void DatePickerTo_Clicked(object sender, System.EventArgs e)
        {
            datePickerTo.IsOpen = true;
        }

        void StatusPicker_OkButtonClicked(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            StatusEntry.Text = e.NewValue.ToString();
        }

        private void StatusPicker_Clicked(object sender, System.EventArgs e)
        {
            statusPicker.IsOpen = true;

        }

    }
}

