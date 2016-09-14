using System;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class AddPlayerPage : ContentPage
	{
		public bool HasBack { get; set; }

		public AddPlayerPage(bool hasBack = true)
		{
			InitializeComponent();

			HasBack = hasBack;
			NavigationPage.SetHasBackButton(this, HasBack);

			Title = Strings.AddPlayer;
			BindingContext = new AddPlayerPageViewModel(HasBack);
		}
	}
}

