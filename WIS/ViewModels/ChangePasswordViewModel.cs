using System;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using WIS.Validators;
using WIS.Validators.Rules;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModel" /> class.
        /// </summary>
        public ChangePasswordViewModel()
        {            
            this.SendCommand = new Command(this.SendClicked);
            password = new ValidatablePair<string>();

            this.Password.Item1.Validations.Add(new IsPinCodeRule<string> { ValidationMessage = "Password need to be 4 digits" });
            this.Password.Item2.Validations.Add(new IsPinCodeRule<string> { ValidationMessage = "Password need to be 4 digits" });
            
        }

        #endregion
        #region Fields

        private ValidatablePair<string> password;
        public ValidatablePair<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }
        

        #endregion

        #region Methods
        public bool AreFieldsValid()
        {
            bool isPhoneValid = this.Password.Validate();
            return isPhoneValid;
        }


        /// <summary>
        /// Invoked when the Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SendClicked(object obj)
        {
            Analytics.TrackEvent(this.GetType().ToString() + " SendClicked");

            if (!AreFieldsValid())
                return;
            APPUSER user = DataService.Instance.CurrentUser;
            DataService.Instance.EditPassword(user.username, password.Item2.Value, (response) =>
            {
                Device.BeginInvokeOnMainThread( async () =>
                {
                    if (response == true)
                    {                        
                        string type = user.user_type;
                        Preferences.Set("TYPE", user.user_type);
                        AppShell page = null;
                        if (type == "STUDENT")
                            page = new AppShell(USERTYPE.STUDENT);
                        else if (type == "PARENT")
                            page = new AppShell(USERTYPE.PARENT);
                        else if (type == "TEACHER")
                            page = new AppShell(USERTYPE.TEACHER);
                        else if (type == "REGISTRAR")
                            page = new AppShell(USERTYPE.REGISTRAR);
                        else if (type == "ADMIN")
                            page = new AppShell(USERTYPE.ADMIN);
                        Application.Current.MainPage = page;
                        await Shell.Current.DisplayAlert("OK", "Password changed", "Ok");
                        if (Application.Current.Properties.ContainsKey("Fcmtocken"))
                        {
                            string token = (string)Application.Current.Properties["Fcmtocken"];
                            DataService.Instance.RegisterFCMToken(token);
                        }
                    }
                    else{
                        await Application.Current.MainPage.DisplayAlert("ERROR", "Phone number not found", "OK");
                    }
                });


            });

        }
        private void LoginClicked(object obj)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }


        #endregion



    }
}
