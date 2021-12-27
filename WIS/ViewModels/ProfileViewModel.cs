using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using WIS.Models;
using WIS.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Model = WIS.Models.UserProfile;

namespace WIS.ViewModels
{
    /// <summary>
    /// ViewModel for Individual profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProfileViewModel : BaseViewModel
    {
        #region Field

        private static ProfileViewModel contactProfileViewModel;

        private ObservableCollection<Model> profileInfo;

        private string profileImage;
       
        private Command editCommand;
        private Command logoutCommand;

        private APPUSER currentUser;
        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class.
        /// </summary>
        public ProfileViewModel(APPUSER user)
        {
            if (user == null)
                currentUser = new APPUSER();
            else
                currentUser = user;
            logoutCommand = new Command(LogoutClicked);
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Gets or sets the value of contact profile view model.
        /// </summary>
        public static ProfileViewModel BindingContext =>
            contactProfileViewModel = PopulateData<ProfileViewModel>("profile.json");

        /// <summary>
        /// Gets or sets a collection of profile info.
        /// </summary>
        [DataMember(Name = "profileInfo")]
        public ObservableCollection<Model> ProfileInfo
        {
            get
            {
                return this.profileInfo;
            }

            set
            {
                this.SetProperty(ref this.profileInfo, value);
            }
        }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        [DataMember(Name = "contactProfileImage")]
        public string ProfileImage
        {
            get { return App.ImageServerPath + this.profileImage; }
            set { this.profileImage = value; }
        }

        public string DisplayName
        {
            get { return currentUser.firstname + " " + currentUser.lastname; }            
        }

        public string UserTitle
        {
            get { return currentUser.user_type; }
        }
        public string Email
        {
            get { return currentUser.email; }
        }

        public string Phone
        {
            get { return currentUser.username; }
        }
        
        public string Birthdate
        {
            get { return currentUser.dob; }
        }

        public string Bio
        {
            get { return currentUser.user_type; }
        }

        public Color colorFromName(string name)
        {
            
            int a = ((int)name.Substring(0, 1).ToCharArray()[0]) / 255; 
            int b = ((int)name.Substring(0, 1).ToCharArray()[0]) / 255;
            int c = ((int)name.Substring(0, 1).ToCharArray()[0]) / 255;
            return Color.FromRgb(a, b, c);
        }

        public StreamImageSource ProfilePicture
        {
            get
            {                
                string initials = currentUser.firstname.Substring(0, 1) + currentUser.lastname.Substring(0, 1);
                return new AvatarImageSource(initials, colorFromName(currentUser.firstname), Color.White,200);

            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command EditCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new Command(this.EditButtonClicked));
            }
        }

      
        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "WIS.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            // Do something
        }

        private void LogoutClicked(object obj)
        {
            Application.Current.MainPage = new LoginPage();
        }
      

        #endregion
    }
}