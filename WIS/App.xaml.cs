using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WIS.Services;
using WIS.Views;

using Xamarin.Essentials;
using WIS.Models;
using System.Collections.Generic;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace WIS
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";


        public static DateTime TimestampToDate(int timestamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddSeconds(timestamp).ToLocalTime();
            return dt;

        }
        public App()
        {         
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTQ5NzgyQDMxMzkyZTM0MmUzMGZiT3RsdmFkQlpuSFFrd1owQnNsWWRwNEs5WU1kaEpJcXdSN01DMmJreDA9");

            AppCenter.Start("ios=17461fcb-db55-4ab1-9523-9bda0d63418f;" +
                  "uwp={Your UWP App secret here};" +
                  "android=1b61a19a-9cac-477d-a836-31039df8ec20;",
                  typeof(Analytics), typeof(Crashes));

            InitializeComponent();

            //MainPage = new SupportDetailPage();
            //Preferences.Set("TYPE", "STUDENT");

            //MainPage = new SignUpPage();
            //MainPage = new SchedulePage();
            //MainPage = new RecentChatPage();
            //MainPage = new ChatMessagePage();

            //MainPage = new AppShell(USERTYPE.STUDENT);
            //MainPage = new SignupValidatedPage(null);
            //MainPage = new ForgotPasswordPage();
            //MainPage = new CardPaymentPage();
            //MainPage = new Test();
            //MainPage = new SupportPage();
            //Preferences.Set("TYPE","STUDENT");
            //MainPage = new ProfilePage();
            //MainPage = new TelegramLoggedPage();

            MainPage = new LoginPage();
            //MainPage = new DemoPage();
            /*
            INVOICE invoice = new INVOICE();
            invoice.issuedate = DateTime.Now;
            invoice.ID = "1";
            invoice.lines = new List<INVOICELINE>()
            {
                new INVOICELINE(){  fee = 20, label = "tuition"},
                new INVOICELINE(){ fee = 180, label = "canteen"},
            };
            */
            //MainPage = new InvoiceListPage();
            //MainPage = new InvoiceDetailsPage(invoice);
            //MainPage = new AttendanceListPage();
            /*
            ABSENCE absence = new ABSENCE()
            {
                ID = "1",
                coursename = "TEST",
                firstname = "firstname",
                lastname = "lastname",
                from = "1628953754",
                to = "1628953754"
            };
            */
            //MainPage = new AttendanceDetailsPage(absence);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        
    }
}
