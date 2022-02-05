using WIS.Validators;
using WIS.Validators.Rules;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using WIS.Views;
using Xamarin.Essentials;
using WIS.Services;
using Plugin.Connectivity;
using Plugin.FirebasePushNotification;

namespace WIS.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : BaseViewModel
    {
        #region Fields

        private ValidatableObject<string> phone;
        private ValidatableObject<string> password;
        private bool isLoading;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="LoginViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
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
        public ValidatableObject<string> Phone
        {
            get{
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
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.IsLoading = false;
            this.Phone = new ValidatableObject<string>();
            this.Password = new ValidatableObject<string>();
            this.Phone.Value = "0964222816";
            this.Password.Value = "2222";            
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Phone.Validations.Add(new IsValidPhoneRule<string> { ValidationMessage = "Phone blank or invalid" });
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            
        }

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {            
            bool isPasswordValid = this.Password.Validate();
            bool isPhoneValid = this.Phone.Validate();
            return isPhoneValid && isPasswordValid;
        }


        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            if (!this.AreFieldsValid())
            {
                return;
            }

            
            if (CrossConnectivity.Current.IsConnected == false)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", "No internet connection", "OK");
                return;
            }            

            // RETRIEVE INFO FROM BACKOFFICE
            IsLoading = true;
            DataService.Instance.Login(Phone.Value, Password.Value, (user) =>
            {
                             
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                    DataService.Instance.CurrentUser = user;
                    
                    
                    if (user != null)
                    {
                        if(user.reset_requested == true)
                        {
                            var page = new ChangePasswordPage();
                            Application.Current.MainPage.Navigation.PushModalAsync(page);
                        }
                        else
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
                            if (Application.Current.Properties.ContainsKey("Fcmtocken"))
                            {
                                string token = (string)Application.Current.Properties["Fcmtocken"];
                                if (CrossFirebasePushNotification.Current != null)
                                {
                                    CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) => {
                                        if (Application.Current != null)
                                            Application.Current.MainPage.DisplayAlert(p.Data["aps.alert.title"].ToString(), p.Data["aps.alert.body"].ToString(), "OK");
                                    };

                                    CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) => {
                                        if (Application.Current != null)
                                            Application.Current.MainPage.DisplayAlert(p.Data["aps.alert.title"].ToString(), p.Data["aps.alert.body"].ToString(), "OK");
                                    };
                                }
                            }                            
                        }
                        
                    }                    
                });                
            });                       
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            SignUpPage page = new SignUpPage();
            Application.Current.MainPage.Navigation.PushModalAsync(page);
            // Do Something
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            ForgotPasswordPage page = new ForgotPasswordPage();
            Application.Current.MainPage.Navigation.PushModalAsync(page);
            // Do something
        }
  
        #endregion
    }
}