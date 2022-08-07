using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using WIS.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(WIS.iOS.DependencyServices.OpenAppiOS))]
namespace WIS.iOS.DependencyServices
{
    public class OpenAppiOS : IAppHandler
    {
        public Task<bool> LaunchApp(string uri, string fallbackuri)
        {
            try
            {
                var canOpen = UIApplication.SharedApplication.CanOpenUrl(new NSUrl(uri));

                if (!canOpen)
                {
                    return Task.FromResult(UIApplication.SharedApplication.OpenUrl(new NSUrl(fallbackuri)));                    
                }
                    

                return Task.FromResult(UIApplication.SharedApplication.OpenUrl(new NSUrl(uri)));

            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}

