using System;
using System.Collections.Generic;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class AttendanceListPage : ContentPage
    {
        public AttendanceListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AttendanceListPageViewModel vm = (AttendanceListPageViewModel)this.BindingContext;
            vm.OnAppearing();
        }
    }
}
