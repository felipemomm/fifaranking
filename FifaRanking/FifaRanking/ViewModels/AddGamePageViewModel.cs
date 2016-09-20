using System;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

using Newtonsoft.Json;

using Firebase.Xamarin.Database;
using System.Collections.ObjectModel;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class AddGamePageViewModel
	{
		public ICommand AddGameCommand { get; set; }

		public FirebaseObject<Player> Team1Player1 { get; set; }

		public FirebaseObject<Player> Team1Player2 { get; set; }

		public int Team1Goals { get; set; }

		public FirebaseObject<Player> Team2Player1 { get; set; }

		public FirebaseObject<Player> Team2Player2 { get; set; }

		public int Team2Goals { get; set; }

		public ObservableCollection<FirebaseObject<Player>> Players { get; set; }

		public bool IsLoading { get; set; }

		public AddGamePageViewModel()
		{
			AddGameCommand = new Command(AddGame);
		}

		public async void LoadPlayers()
		{
			IsLoading = true;

			var players = await App.Instance.RankingManager.GetPlayers();

			Players = new ObservableCollection<FirebaseObject<Player>>(players);

			IsLoading = false;
		}

		private async void AddGame()
		{
			IsLoading = true;

			if (AreFieldsValid())
			{
				try
				{
					Game game = new Game();
					game.Date = DateTime.Now;

					game.Team1Player1 = Team1Player1.Key;
					game.Team1Player2 = Team1Player2.Key;
					game.Team1Goals = Team1Goals;

					game.Team2Player1 = Team2Player1.Key;
					game.Team2Player2 = Team2Player2.Key;
					game.Team2Goals = Team2Goals;

					await App.Instance.RankingManager.AddGame(game);

					var players = await App.Instance.RankingManager.GetPlayers();
					App.Instance.RankingManager.UpdatePlayerStats(game, players);
					await App.Instance.RankingManager.UpdatePlayers(players);

					await App.DisplayAlertAsync("Game added.");

					await ((MainPage)App.Instance.MainPage).PopAsync();
				}
				catch
				{
					await App.DisplayAlertAsync("Add Game failed.");
				}
			}

			IsLoading = false;
		}

		private bool AreFieldsValid()
		{
			bool valid = Team1Player1 != null && Team1Player2 != null 
				&& Team2Player1 != null && Team2Player2 != null;
			if (!valid)
			{
				Device.BeginInvokeOnMainThread(async () => await App.DisplayAlertAsync("Please fill all fields."));
				return valid;
			}

			if (Team1Player1 == Team1Player2
			    || Team1Player1 == Team2Player1
				|| Team1Player1 == Team2Player2
				|| Team1Player2 == Team2Player1
				|| Team1Player2 == Team2Player2
				|| Team2Player1 == Team2Player2)
			{
				Device.BeginInvokeOnMainThread(async () => await App.DisplayAlertAsync("Players should be chosen only once."));
				return false;
			}

			return valid;
		}
	}
}