using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace PrescriptionFiller.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

            App.ScreenWidth = (int)(UIScreen.MainScreen.Bounds.Width * UIScreen.MainScreen.Scale);
            App.ScreenHeight = (int)(UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale);
            App.ScreenDensity = (float)UIScreen.MainScreen.Scale;

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

