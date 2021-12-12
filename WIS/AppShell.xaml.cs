using System;
using System.Collections.Generic;
using WIS.ViewModels;
using WIS.Views;
using Xamarin.Forms;

namespace WIS
{
    public enum USERTYPE
    {
        STUDENT,
        PARENT,
        TEACHER,
        REGISTRAR
    };


    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        ShellContent profile;
        Tab attendance;
        Tab invoices;
        ShellContent schedule;
        ShellContent support;

        public void initShells(Style style)
        {
            this.profile = new ShellContent()
            {
                Route = "Profile",
                Title = "Profile",
                Icon = nameof(ProfilePage),
                ContentTemplate = new DataTemplate(typeof(ProfilePage)),
                Style = style
            };

            // ATTENDANCE 
            this.attendance = new Tab()
            {
                Title = "Attendance",
                Route = "Attendance",
                Icon = "AttendanceListPage"
            };

            this.attendance.Items.Add(new ShellContent()
            {
                Route = "AttendanceNew",
                Title = "New",
                Icon = nameof(AttendanceListPage),
                ContentTemplate = new DataTemplate(typeof(AttendanceListPage)),
                Style = style
            });

            this.attendance.Items.Add(new ShellContent()
            {
                Route = "AttendanceHistory",
                Title = "History",
                Icon = nameof(AttendanceListHistoryPage),
                ContentTemplate = new DataTemplate(typeof(AttendanceListHistoryPage)),
                Style = style
            });

            // INVOICE
            this.invoices = new Tab()
            {
                Title = "Invoices",
                Route = "Invoices",
                Icon = "InvoiceListPage"
            };
            this.invoices.Items.Add(new ShellContent()
            {
                Route = "InvoiceNew",
                Title = "New",
                Icon = nameof(InvoiceListPage),
                ContentTemplate = new DataTemplate(typeof(InvoiceListPage)),
                Style = style
            });

            this.invoices.Items.Add(new ShellContent()
            {
                Route = "InvoiceHistory",
                Title = "History",
                Icon = nameof(InvoiceListHistoryPage),
                ContentTemplate = new DataTemplate(typeof(InvoiceListHistoryPage)),
                Style = style
            });

           
            this.schedule = new ShellContent()
            {
                Route = "Schedule",
                Title = "Schedule",
                Icon = nameof(SchedulePage),
                ContentTemplate = new DataTemplate(typeof(SchedulePage)),
                Style = style
            };
            
        }
        public AppShell(USERTYPE type)
        {
            InitializeComponent();
            
            Routing.RegisterRoute("AttendanceDetails", typeof(AttendanceDetailsPage));
            Routing.RegisterRoute("InvoiceDetails", typeof(InvoiceDetailsPage));                        
            //Routing.RegisterRoute("SupportDetails", typeof(SupportDetailPage));

       
            Style style = null;
            style = StudentShell;
            initShells(style);
            //FlyoutBackgroundColor = Color.FromHex("1746A0");
            FlyoutBackgroundColor = Color.Red;
            BackgroundColor = Color.Red;
            
            if (type == USERTYPE.PARENT)
            {                                
                itemContainer.Items.Add(attendance);
                itemContainer.Items.Add(invoices);
                itemContainer.Items.Add(support);
                itemContainer.Items.Add(profile);                
            }                
            else if (type == USERTYPE.STUDENT)
            {                                
                itemContainer.Items.Add(schedule);
                itemContainer.Items.Add(support);
                itemContainer.Items.Add(profile);
                
            }                
            else if (type == USERTYPE.TEACHER)
            {                                                
                itemContainer.Items.Add(schedule);
                itemContainer.Items.Add(support);
                itemContainer.Items.Add(profile);                
            }                
            else if (type == USERTYPE.REGISTRAR)
            {                
                itemContainer.Items.Add(support);
                itemContainer.Items.Add(invoices);
                itemContainer.Items.Add(profile);                
            }
            CurrentItem = profile;                       
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            

        }
    }
}
