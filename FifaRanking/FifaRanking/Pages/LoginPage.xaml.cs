using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			NavigationPage.SetHasBackButton(this, false);
			NavigationPage.SetHasNavigationBar(this, false);

			Title = Strings.Login;
			BindingContext = new LoginPageViewModel();
		}
	}
}

