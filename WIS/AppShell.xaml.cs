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
        REGISTRAR,
        ADMIN
    };


    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        ShellContent profile;
        Tab attendance;
        Tab invoices;
        Tab invoicesregistrar;
        ShellContent studentschedule;
        ShellContent teacherschedule;
        ShellContent parentschedule;
        ShellContent push;

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

            // INVOICE (Registrar)
            this.invoicesregistrar = new Tab()
            {
                Title = "Invoices(Registar)",
                Route = "InvoicesRegistrar",
                Icon = "InvoiceListPage"
            };

            this.invoicesregistrar.Items.Add(new ShellContent()
            {
                Route = "PaymentListSearch",
                Title = "Search payments",
                Icon = "Search",
                ContentTemplate = new DataTemplate(typeof(PaymentListSearchPage)),
                Style = style
            });

            this.invoicesregistrar.Items.Add(new ShellContent()
            {
                Route = "PaymentListToValidate",
                Title = "To validate",
                Icon = "InvoiceListPage",
                ContentTemplate = new DataTemplate(typeof(PaymentListToValidatePage)),
                Style = style
            });

            


            // INVOICE
            this.invoices = new Tab()
            {
                Title = "Invoice",
                Route = "Invoices",
                Icon = "InvoiceListPage"
            };

            
            this.invoices.Items.Add(new ShellContent()
            {
                Route = "InvoiceNew",
                Title = "Unpaid",
                Icon = nameof(InvoiceListPage),
                ContentTemplate = new DataTemplate(typeof(InvoiceListPage)),
                Style = style
            });
            this.invoices.Items.Add(new ShellContent()
            {
                Route = "InvoiceHistory",
                Title = "Paid",
                Icon = nameof(InvoiceListHistoryPage),
                ContentTemplate = new DataTemplate(typeof(InvoiceListHistoryPage)),
                Style = style
            });
            // SCHEDULE
            this.studentschedule = new ShellContent()
            {
                Route = "StudentSchedule",
                Title = "Schedule",
                Icon = "SchedulePage",
                ContentTemplate = new DataTemplate(typeof(StudentSchedulePage)),                
                Style = style
            };
            // TEACHERSCHEDULE
            this.teacherschedule = new ShellContent()
            {
                Route = "TeacherSchedule",
                Title = "Schedule",
                Icon = "SchedulePage",
                ContentTemplate = new DataTemplate(typeof(TeacherSchedulePage)),
                Style = style
            };
            // PARENTSCHEDULE
            this.parentschedule = new ShellContent()
            {
                Route = "ParentSchedule",
                Title = "Schedule",
                Icon = "SchedulePage",
                ContentTemplate = new DataTemplate(typeof(ParentSchedulePage)),
                Style = style
            };


            this.push = new ShellContent()
            {
                Route = "Push",
                Title = "Push",
                Icon = "PushPage",
                ContentTemplate = new DataTemplate(typeof(SendPushNotificationPage)),
                Style = style
            };
            
        }
        public AppShell(USERTYPE type)
        {
            InitializeComponent();
                        
            Routing.RegisterRoute("InvoiceDetails", typeof(InvoiceDetailsPage));
            Routing.RegisterRoute("InvoiceDetailsPaid", typeof(InvoiceDetailsPaidPage));

            Routing.RegisterRoute("PaymentABADetails", typeof(PaymentABADetailsPage));

            Style style = null;
            style = CommonShell;
            initShells(style);
            FlyoutBackgroundColor = Color.FromHex("1746A0");
            BackgroundColor = Color.FromHex("1746A0");
            
            if (type == USERTYPE.PARENT)
            {                                
                itemContainer.Items.Add(attendance);
                itemContainer.Items.Add(parentschedule);
                itemContainer.Items.Add(invoices);
                itemContainer.Items.Add(profile);                
            }                
            else if (type == USERTYPE.STUDENT)
            {
                itemContainer.Items.Add(studentschedule);
                itemContainer.Items.Add(profile);                
            }                
            else if (type == USERTYPE.TEACHER)
            {                                                
                itemContainer.Items.Add(teacherschedule);
                itemContainer.Items.Add(profile);                
            }                
            else if (type == USERTYPE.REGISTRAR)
            {                
                itemContainer.Items.Add(invoices);
                itemContainer.Items.Add(profile);                
            }
            else if(type == USERTYPE.ADMIN)
            {
                itemContainer.Items.Add(attendance);
                itemContainer.Items.Add(invoices);
                itemContainer.Items.Add(invoicesregistrar);
                itemContainer.Items.Add(studentschedule);
                itemContainer.Items.Add(teacherschedule);
                itemContainer.Items.Add(parentschedule);
                itemContainer.Items.Add(push);
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
