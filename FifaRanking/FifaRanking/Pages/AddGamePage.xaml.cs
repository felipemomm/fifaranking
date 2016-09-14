using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FifaRanking
{
	public partial class AddGamePage : ContentPage
	{
		private AddGamePageViewModel viewModel;

		public AddGamePage()
		{
			InitializeComponent();

			Title = Strings.AddGame;
			viewModel = new AddGamePageViewModel();
			BindingContext = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadPlayers();
		}
	}
}

