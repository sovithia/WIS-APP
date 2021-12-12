using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WIS.Views
{
    public partial class PictureZoomPage : ContentPage
    {
        public PictureZoomPage()
        {
            InitializeComponent();
        }
        public PictureZoomPage(ImageSource source)
        {
            InitializeComponent();
            image.Source = source;
        }

        void close(System.Object sender, System.EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}
