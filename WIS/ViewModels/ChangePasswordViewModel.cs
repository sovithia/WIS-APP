using System;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using WIS.Validators;
using WIS.Validators.Rules;
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
            password = new ValidatableObject<string>();
            
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password empty" });
            this.Password.Validations.Add(new IsValidPhoneRule<string> { ValidationMessage = "Passwords does not match" });
        }

        #endregion
        #region Fields

        private ValidatableObject<string> password;
        public ValidatableObject<string> Password
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
            DataService.Instance.EditPassword(user.id, password.Value, user.remember_me_token, (response) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (response == true)
                    {
                        Shell.Current.DisplayAlert("OK", "Password changed", "Ok");                        
                    }
                    else{
                        Application.Current.MainPage.DisplayAlert("ERROR", "Phone number not found", "OK");
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
