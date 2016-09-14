using System;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class PlayersPage : ContentPage
	{
		private PlayersPageViewModel viewModel;

		public PlayersPage()
		{
			InitializeComponent();

			Title = Strings.Players;
			viewModel = new PlayersPageViewModel();
			BindingContext = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadPlayers();
		}
	}
}

