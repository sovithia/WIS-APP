using System;
using System.Collections.Generic;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class TestPopup : ContentPage
    {
        public TestPopup()
        {
            InitializeComponent();
            popUpLayout.PopupView.AnimationMode = AnimationMode.SlideOnBottom;
         
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            popUpLayout.Show(0, 700);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                popUpLayout.Show(0, 600);
            }catch(Exception ex)
            {
                int i = 2;
            }
            
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            try
            {
                popUpLayout.Dismiss();
            }
            catch (Exception ex)
            {
                int i = 2;
            }

        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}

