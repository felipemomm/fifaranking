using System;
using System.Threading.Tasks;

using Plugin.Settings;
using Plugin.Settings.Abstractions;

using Xamarin.Forms;

namespace FifaRanking
{
	public class App : Application
	{
		public AuthManager AuthManager { get; set; }

		public RankingManager RankingManager { get; set; }

		public App()
		{
			AuthManager = new AuthManager();
			RankingManager = new RankingManager();

			// The root page of your application
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public static App Instance
		{
			get
			{
				return (App)Application.Current;
			}
		}

		public static ISettings Settings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		public static async Task DisplayAlertAsync(string message, string title = null, string button = null)
		{
			await App.Instance.MainPage.DisplayAlert(title ?? Strings.AppName, message, button ?? Strings.OK);
		}
	}
}