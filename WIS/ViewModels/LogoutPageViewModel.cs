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

        public  void logout(Object obj)
        {
            Application.Current.MainPage = new LoginPage();
            /*
            var answer = AppShell.Current.DisplayAlert("Confirmation", "Do you want to Log out?", "Yes", "No");
            if (answer)
            {
            
            }
            */
        }
    }
}
