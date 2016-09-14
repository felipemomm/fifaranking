﻿using System;

using Foundation;
using UIKit;

namespace FifaRanking.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
		{
			Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(uiApplication, launchOptions);
		}
	}
}

