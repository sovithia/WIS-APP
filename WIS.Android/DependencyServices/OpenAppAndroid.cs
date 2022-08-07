using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using WIS.Interfaces;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(WIS.Droid.DependencyServices.OpenAppAndroid))]
namespace WIS.Droid.DependencyServices
{
    [Activity(Label = "OpenAppAndroid")]
    public class OpenAppAndroid : Activity, IAppHandler
    {
        public async Task<bool> LaunchApp(string uri, string fallbackurl)
        {
            bool result = false;

            try
            {
                var supportsUri = await Launcher.CanOpenAsync(uri);
                if (supportsUri)
                {
                    await Launcher.OpenAsync(uri);
                    result = true;
                }
                else
                {
                    await Launcher.OpenAsync(fallbackurl);
                    result = false;
                }                         
            }
            catch (ActivityNotFoundException)
            {
                result = false;
            }

            return result;
        }

     
    }
}

