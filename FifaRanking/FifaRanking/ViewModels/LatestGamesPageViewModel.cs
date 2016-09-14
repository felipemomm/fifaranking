using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

using Firebase.Xamarin.Database;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class LatestGamesPageViewModel
	{
		public ObservableCollection<FirebaseObject<Game>> Games { get; set; }

		public bool IsLoading { get; set; }

		public LatestGamesPageViewModel()
		{
		}

		public async void LoadGames()
		{
			IsLoading = true;

			var games = await App.Instance.RankingManager.GetLatestGames();
			var players = await App.Instance.RankingManager.GetPlayers();

			foreach (var game in games)
			{
				game.Object.Team1Player1 = 
					players.FirstOrDefault(p => p.Key.Equals(game.Object.Team1Player1)).Object.Name;
				
				game.Object.Team1Player2 = 
					players.FirstOrDefault(p => p.Key.Equals(game.Object.Team1Player2)).Object.Name;
				
				game.Object.Team2Player1 = 
					players.FirstOrDefault(p => p.Key.Equals(game.Object.Team2Player1)).Object.Name;
				
				game.Object.Team2Player2 = 
					players.FirstOrDefault(p => p.Key.Equals(game.Object.Team2Player2)).Object.Name;
			}

			Games = new ObservableCollection<FirebaseObject<Game>>(games);

			IsLoading = false;
		}
	}
}