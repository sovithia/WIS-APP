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
            phone = new ValidatableObject<string>();
            
            this.Phone.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Phone Required" });
            this.Phone.Validations.Add(new IsValidPhoneRule<string> { ValidationMessage = "Invalid Phone" });
        }

        #endregion
        #region Fields

        private ValidatableObject<string> phone;
        public ValidatableObject<string> Phone
        {
            get
            {
                return this.phone;
            }

            set
            {
                if (this.phone == value)
                {
                    return;
                }

                this.SetProperty(ref this.phone, value);
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
            bool isPhoneValid = this.Phone.Validate();
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
            DataService.Instance.EditPassword(user.id, "",user.remember_me_token, (response) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (response == true)
                    {
                       //var page = new PasswordSentPage();
                       //Application.Current.MainPage.Navigation.PushModalAsync(page);
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
