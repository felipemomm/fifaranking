using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class RankingPage : ContentPage
	{
		private RankingPageViewModel viewModel;

		public RankingPage()
		{
			InitializeComponent();

			Title = Strings.Ranking;
			viewModel = new RankingPageViewModel();
			BindingContext = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadPlayers();
		}
	}
}

