using WIS.Validators;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using WIS.Views;
using WIS.Services;
using System;
using WIS.Validators.Rules;
using Microsoft.AppCenter.Analytics;

namespace WIS.ViewModels
{
    /// <summary>
    /// ViewModel for forgot password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ForgotPasswordViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModel" /> class.
        /// </summary>
        public ForgotPasswordViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
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

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        #endregion

        #region Methods
        public bool AreFieldsValid()
        {           
            bool isPhoneValid = this.Phone.Validate();            

            return  isPhoneValid;
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
            DataService.Instance.ForgotPassword(Phone.Value, (response) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (response == true)
                    {
                        var page = new PasswordSentPage();
                        Application.Current.MainPage.Navigation.PushModalAsync(page);
                    }
                    else
                    {
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