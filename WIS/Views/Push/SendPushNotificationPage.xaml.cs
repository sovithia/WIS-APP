using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.Views
{
    public partial class SendPushNotificationPage : ContentPage
    {
        public SendPushNotificationPage()
        {
            InitializeComponent();
        }

        /*
        void BtnSend_Clicked(System.Object sender, System.EventArgs e)
        {            
            try
            {
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                if (FCMToken)
                {
                    var FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                    FCMBody body = new FCMBody();
                    FCMNotification notification = new FCMNotification();
                    notification.title = "Xamarin Forms FCM Notifications";
                    notification.body = "Sample For FCM Push Notifications in Xamairn Forms";
                    FCMData data = new FCMData();
                    data.key1 = "";
                    data.key2 = "";
                    data.key3 = "";
                    data.key4 = "";
                    body.registration_ids = new[] { FCMTockenValue };
                    body.notification = notification;
                    body.data = data;
                    var isSuccessCall = SendNotification(body).Result;
                    if (isSuccessCall)
                    {
                        DisplayAlert("Alert", "Notifications Send Successfully", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Alert", "Notifications Send Failed", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<bool> SendNotification(FCMBody fcmBody)
        {
            try
            {
                var httpContent = JsonConvert.SerializeObject(fcmBody);
                var client = new HttpClient();
                var authorization = string.Format("key={0}", "AAAAdo7memY:APA91bHNfseEKErXXXXXXX");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                var stringContent = new StringContent(httpContent);
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string uri = "https://fcm.googleapis.com/fcm/send";
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                var result = response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (TaskCanceledException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        */
    }
}
