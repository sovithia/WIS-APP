using Android.App;
using Android.Content;
using Firebase.Iid;
using System;


namespace WIS.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    [Obsolete]
    public class FirebaseInstanceIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        // [START refresh_token]
        [Obsolete]
        public override void OnTokenRefresh()
        {
            // Get updated InstanceID token.
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Android.Util.Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            System.Diagnostics.Debug.WriteLine($"######Token######  :  {refreshedToken}");
            Xamarin.Forms.Application.Current.Properties["Fcmtocken"] = FirebaseInstanceId.Instance.Token ?? "";
            Xamarin.Forms.Application.Current.SavePropertiesAsync();
        }
        // [END refresh_token] 
    }
}
