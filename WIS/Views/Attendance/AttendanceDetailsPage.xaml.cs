using System;
using System.Collections.Generic;
using WIS.Models;
using WIS.ViewModels;
using Xamarin.Forms;

namespace WIS.Views
{
    [QueryProperty(nameof(ID), "ID"),QueryProperty(nameof(IsSubmitable), "IsSubmitable")]
    public partial class AttendanceDetailsPage : ContentPage
    {
        public string ID
        {
            set
            {
                this.BindingContext = new AttendanceDetailsPageViewModel(value);
            }
        }

        public bool IsSubmitable
        {
            set
            {
                AttendanceDetailsPageViewModel vm = (AttendanceDetailsPageViewModel)this.BindingContext;
                vm.IsSubmitable = value;
            }
        }


        public AttendanceDetailsPage()
        {
            try
            {
                InitializeComponent();
            }catch(Exception ex)
            {
                int i = 2;
            }
            
        }
       
    }
}
