using System;
using Microsoft.AppCenter.Analytics;
using WIS.Models;
using WIS.Services;
using WIS.Validators;
using WIS.Validators.Rules;
using WIS.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WIS.ViewModels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : BaseViewModel
    {

        #region Fields

        private ValidatableObject<string> identifier;

        private ValidatableObject<string> phone;

        private ValidatableObject<string> type;

        private ValidatableObject<string> birthDate;

        private bool isLoading;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
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
                this.SetProperty(ref this.isLoading, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> Identifier
        {
            get
            {
                return this.identifier;
            }
            set
            {
                if (this.identifier == value)
                {
                    return;
                }

                this.SetProperty(ref this.identifier, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the birthdate from users in the Sign Up page.
        /// </summary>
        ///
        public ValidatableObject<string> BirthDate
        {
            get{
                
                 return this.birthDate;
            }
            set{
                if (this.birthDate == value)
                {
                    return;
                }
                this.SetProperty(ref this.birthDate, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the birthdate from users in the Sign Up page.
        /// </summary>
        ///
        public ValidatableObject<string> Type
        {
            get
            {
                return this.type;
            }
            set
            {
                if (this.type == value)
                {
                    return;
                }
                this.SetProperty(ref this.type, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the phone from users in the Sign Up page.
        /// </summary>       
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
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command ShowDatePicker{ get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {                        
            bool isPhoneValid = this.Phone.Validate();
            bool isIdentifierValid = this.Identifier.Validate();
            bool isBirthdateValid = this.BirthDate.Validate();
            bool isTypeValid = this.Type.Validate();
            return isPhoneValid && isIdentifierValid && isBirthdateValid && isTypeValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.IsLoading = false;
            this.Identifier = new ValidatableObject<string>();
            this.Phone = new ValidatableObject<string>();
            this.BirthDate = new ValidatableObject<string>() { Value = "Birthdate" };
            this.Type = new ValidatableObject<string>() { Value = "Type" };
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.Identifier.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Identifier required" });
            this.Phone.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Phone Required" });
            this.Phone.Validations.Add(new IsValidPhoneRule<string> { ValidationMessage = "Invalid Phone" });
            this.BirthDate.Validations.Add(new isValidDateRule<string> { ValidationMessage = "Invalid Date" });

            this.Type.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Invalid Type" });
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
                PRESIGNUPREQUEST signupRequest = new PRESIGNUPREQUEST();
                signupRequest.type = Type.Value;
                signupRequest.identifier = Identifier.Value;
                signupRequest.phone = Phone.Value;
                signupRequest.birthdate = BirthDate.Value;
                IsLoading = true;
                DataService.Instance.PreSignup(signupRequest, (success) =>
                {                    
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                        if (success == true)
                        {
                            var page = new SignupValidatedPage(signupRequest);
                            Application.Current.MainPage.Navigation.PushModalAsync(page);
                        }                        
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
            Application.Current.MainPage.Navigation.PopModalAsync();

        }
        #endregion
        

    }
}