using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class AttendanceListHistoryPage : ContentPage
    {
        public AttendanceListHistoryPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
       
            base.OnAppearing();
            AttendanceListHistoryPageViewModel vm = (AttendanceListHistoryPageViewModel)this.BindingContext;
            vm.OnAppearing();
            
        }
    }
}
