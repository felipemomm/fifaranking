using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class LatestGamesPage : ContentPage
	{
		private LatestGamesPageViewModel viewModel;

		public LatestGamesPage()
		{
			InitializeComponent();

			Title = Strings.LatestGames;
			viewModel = new LatestGamesPageViewModel();
			BindingContext = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadGames();
		}

		protected void OnTapped(object sender, EventArgs args)
		{
			this.listView.SelectedItem = null;
		}
	}
}