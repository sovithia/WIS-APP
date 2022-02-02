using System;
using WIS.Services;
using WIS.Validators;
using WIS.Validators.Rules;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class SendPushNotificationPageViewModel : BaseViewModel
    {
        #region Fields

        private ValidatableObject<string> pushtitle;
        private ValidatableObject<string> pushbody;
        private bool isLoading;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="LoginViewModel" /> class.
        /// </summary>
        public SendPushNotificationPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.SendCommand = new Command(this.SendClicked);
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
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>
        public ValidatableObject<string> PushTitle
        {
            get
            {
                return this.pushtitle;
            }

            set
            {
                if (this.pushtitle == value)
                {
                    return;
                }

                this.SetProperty(ref this.pushtitle, value);
            }
        }

        public ValidatableObject<string> PushBody
        {
            get
            {
                return this.pushbody;
            }

            set
            {
                if (this.pushbody == value)
                {
                    return;
                }
                this.SetProperty(ref this.pushbody, value);
            }
        }
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.IsLoading = false;
            this.pushtitle = new ValidatableObject<string>();
            this.pushbody = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.PushTitle.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Title Required" });
            this.PushBody.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });

        }

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            bool isTitleValid = this.PushTitle.Validate();
            bool isBodyValid = this.PushBody.Validate();
            return isTitleValid && isBodyValid;
        }

        private void SendClicked(object obj)
        {
            DataService.Instance.SendPushNotification(this.pushtitle.Value,this.pushbody.Value, (response) =>{
                
                if (response)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Shell.Current.DisplayAlert("OK", "Push Notification sent", "Ok");
                    });
                }
                    
            });
        }
        #endregion
    }
}