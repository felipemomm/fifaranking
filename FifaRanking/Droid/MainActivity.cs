using System;

using Android.App;
using Android.Content.PM;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace FifaRanking.Droid
{
	[Activity(Label = "Fifinha Ranking", Theme = "@style/Theme.Splash", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
	public class SplashActivity : FormsApplicationActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Forms.Init(this, savedInstanceState);

			RequestedOrientation = ScreenOrientation.SensorPortrait;

			StartActivity(typeof(MainActivity));
		}
	}

	[Activity(Label = "Fifinha Ranking", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Forms.Init(this, savedInstanceState);

			LoadApplication(new App());

			RequestedOrientation = ScreenOrientation.SensorPortrait;
		}

		public override void OnBackPressed()
		{
			var currentPage = this.GetCurrentPage();

			if ((currentPage is AddPlayerPage) && !(((AddPlayerPage)currentPage).HasBack))
			{
				// Do nothing
			}
			else if ((currentPage is LoginPage) || currentPage is HomePage)
			{
				this.MoveTaskToBack(true);
			}
			else
			{
				base.OnBackPressed();
			}
		}

		private Page GetCurrentPage()
		{
			MainPage mainPage = App.Instance.MainPage as MainPage;
			if (mainPage == null)
			{
				return null;
			}

			NavigationPage navigationPage = mainPage.Detail as NavigationPage;
			if (navigationPage == null)
			{
				return null;
			}

			return navigationPage.CurrentPage;
		}
	}
}