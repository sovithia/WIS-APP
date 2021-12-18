using System;
using WIS.Models;
using WIS.Services;
using WIS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace WIS.Views
{
    /// <summary>
    /// Page to show Contact profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage
    {
        public ProfilePage()
        {          
            APPUSER user = DataService.Instance.CurrentUser;
            this.BindingContext = new ProfileViewModel(user);
            try
            {
                this.InitializeComponent();
            }catch(Exception ex)
            {
                int i = 2;
            }
            
        }
    }
}