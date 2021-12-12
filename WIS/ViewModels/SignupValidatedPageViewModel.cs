using System;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using WIS.Validators;
using WIS.Validators.Rules;
using WIS.Views;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class SignupValidatedPageViewModel : BaseViewModel
    {
        #region Fields
        
        private ValidatablePair<string> password;
        private PRESIGNUPREQUEST presignup;
        private bool isLoading;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignupValidatedPageViewModel(PRESIGNUPREQUEST _presignup)
        {
            presignup = _presignup;
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }
        #endregion

        #region Property

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                if (this.isLoading == value)
                {
                    return;
                }

                this.SetProperty(ref this.isLoading, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
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
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

      
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {
            bool isPasswordValid = this.Password.Validate();


            return isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.IsLoading = false;
            this.Password = new ValidatablePair<string>();
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {            
            this.Password.Item1.Validations.Add(new IsPinCodeRule<string> { ValidationMessage = "Password need to be 4 digits" });
            this.Password.Item2.Validations.Add(new IsPinCodeRule<string> { ValidationMessage = "Password need to be 4 digits" });
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            Analytics.TrackEvent(this.GetType().ToString() + " SignUpClicked");
            
            if (this.AreFieldsValid())
            {
                SIGNUPREQUEST signupRequest = new SIGNUPREQUEST();
                signupRequest.type = presignup.type;
                signupRequest.identifier = presignup.identifier;
                signupRequest.phone = presignup.phone;
                signupRequest.birthdate = presignup.birthdate;
                signupRequest.password = Password.Item1.Value;
                IsLoading = true;
                DataService.Instance.Signup(signupRequest, (success) =>
                {                    
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                        if (success == true)
                        {
                            var page = new SuccessfulSignupPage();
                            Application.Current.MainPage.Navigation.PushModalAsync(page);
                        }
                        else
                            Application.Current.MainPage.DisplayAlert("ERROR", "Wrong information on signup, please check again", "OK");
                    });
                });
            }
            
        }

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            AppShell.Current.Navigation.PopToRootAsync();            

        }
        #endregion
    }
}
