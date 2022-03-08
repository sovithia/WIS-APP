using System;
using WIS.Views;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class LogoutPageViewModel : BaseViewModel
    {
        public LogoutPageViewModel()
        {
            LogoutCommand = new Command(logout);
        }

        public Command LogoutCommand { get; set; }

        public async void logout(Object obj)
        {                        
            var answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you want to Log out?", "Yes", "No");
            if (answer)
            {
                Application.Current.MainPage = new LoginPage();
            }
            
        }
    }
}
