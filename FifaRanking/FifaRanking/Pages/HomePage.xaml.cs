using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();

			Title = Strings.AppName;
			NavigationPage.SetBackButtonTitle(this, "");
		}

		protected override async void OnAppearing()
		{
			if (!App.Instance.AuthManager.IsAuthenticated)
			{
				await ((MainPage)App.Instance.MainPage).PushAsync(new LoginPage(), false);
			}
		}
	}
}