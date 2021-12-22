using System.Collections.Generic;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace WIS.Views
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            this.InitializeComponent();
            typePicker.SelectionChanged += Typepicker_SelectionChanged;
            typePicker.ItemsSource = new List<string>()
            {
                "STUDENT",
                "PARENT",
                "TEACHER",
                "REGISTRAR",
                "ADMIN"

            };
        }

        private void DatePicker_Clicked(object sender, System.EventArgs e)
        {
            datePicker.IsOpen = true;
            
        
        }

        private void TypePicker_Clicked(object sender, System.EventArgs e)
        {
            typePicker.IsOpen = true;


        }

        private void Typepicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            string val = e.NewValue.ToString();
            if (val == "STUDENT"){
                IdentifierEntry.Placeholder = "Student ID";
                BirthdateEntry.Text = "Birthdate";
            }
            else if (val == "PARENT"){
                IdentifierEntry.Placeholder = "Student ID";
                BirthdateEntry.Text = "Student Birthdate";
            }
            else if (val == "TEACHER"){
                IdentifierEntry.Placeholder = "Employee Number";
                BirthdateEntry.Text = "Birthdate";
            }
            else if (val == "REGISTRAR"){
                IdentifierEntry.Placeholder = "Employee Number";
                BirthdateEntry.Text = "Birthdate";
            }else if (val == "ADMIN"){
                IdentifierEntry.Placeholder = "Employee Number";
                BirthdateEntry.Text = "Birthdate";
            }


        }

        private void DatePicker_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            BirthdateEntry.Text = string.Format("{0:yyyy-MM-dd}", e.NewValue);
        }

        
        void TypePicker_OkButtonClicked(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TypeEntry.Text = e.NewValue.ToString();
        }

    }
}