using System;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class SideMenuPage : ContentPage
	{
		public SideMenuPage()
		{
			InitializeComponent();

			Icon = "menu";
			Title = Strings.Menu;

			BindingContext = new SideMenuPageViewModel();
		}
	}
}

