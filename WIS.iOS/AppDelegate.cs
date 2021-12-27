using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Syncfusion.SfSchedule.XForms.iOS;
using Syncfusion.XForms.iOS.DataForm;
using MediaManager;
using Syncfusion.XForms.iOS.SignaturePad;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using UserNotifications;
using Firebase.CloudMessaging;
using Plugin.FirebasePushNotification;

namespace WIS.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Firebase.Core.App.Configure();
            global::Xamarin.Forms.Forms.Init();
            SfEffectsViewRenderer.Init();
            SfDatePickerRenderer.Init();
            SfCardViewRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfRatingRenderer.Init();
            SfComboBoxRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfAvatarViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfListViewRenderer.Init();
            Core.Init();
            SfGradientViewRenderer.Init();
            SfBorderRenderer.Init();
            SfButtonRenderer.Init();
            SfCheckBoxRenderer.Init();

            SfScheduleRenderer.Init();
            SfDataFormRenderer.Init();
            SfPickerRenderer.Init();
            SfSignaturePadRenderer.Init();

            CrossMediaManager.Current.Init();

            new SfBusyIndicatorRenderer();

            LoadApplication(new App());
            

            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                var textColor = UIColor.Red;

                UINavigationBar.Appearance.ScrollEdgeAppearance = new UINavigationBarAppearance() { BackgroundColor = UIColor.FromRGB(33, 150, 234) };
                UINavigationBar.Appearance.ScrollEdgeAppearance.TitleTextAttributes.ForegroundColor = textColor; // will be ignored and Title is ALLWAYS black
                UITabBar.Appearance.ScrollEdgeAppearance = new UITabBarAppearance { BackgroundColor = UIColor.FromRGB(33, 150, 234) };
            }

            // PUSH
            RegisterForRemoteNotifications();
            Messaging.SharedInstance.Delegate = this;
            if (UNUserNotificationCenter.Current != null)
            {
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            }

            FirebasePushNotificationManager.Initialize(options, true);

            return base.FinishedLaunching(app, options);
        }

        private void RegisterForRemoteNotifications()
        {

            // Register your app for remote notifications.
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            

        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);            
            System.Console.WriteLine(userInfo); // 
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            Xamarin.Forms.Application.Current.Properties["Fcmtocken"] = Messaging.SharedInstance.FcmToken ?? "";
            Xamarin.Forms.Application.Current.SavePropertiesAsync();
            System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
            Console.WriteLine(fcmToken);

        }

    }
}

