using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using Foundation;
using UIKit;

namespace WIS.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            try
            {
                //AppDomain currentDomain = AppDomain.CurrentDomain;
                //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch(Exception ex)
            {
                int i = 2;
            }
            

        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }
    }
}
