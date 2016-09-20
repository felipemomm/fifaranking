using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using PropertyChanged;

using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Streaming;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class LatestGamesPageViewModel
	{
		private const int MAX_GAMES = 10;

		public ObservableCollection<FirebaseObject<Game>> Games { get; set; }

		public bool IsLoading { get; set; }

		private List<FirebaseObject<Player>> players;

		public LatestGamesPageViewModel()
		{
			App.Instance.RankingManager.GetPlayerByStreaming().Subscribe(RefreshPlayer);
			App.Instance.RankingManager.GetLatestGamesByStreaming().Subscribe(RefreshGame);
		}

		public async void LoadGames()
		{
			IsLoading = true;

			var games = await App.Instance.RankingManager.GetLatestGames(MAX_GAMES);
			players = await App.Instance.RankingManager.GetPlayers();

			RefreshGames(games);

			IsLoading = false;
		}

		private void RefreshPlayer(FirebaseEvent<Player> player)
		{
			if (!players.Any(p => p.Key.Equals(player.Key)))
			{
				players.Add(player);
			}
		}

		private void RefreshGame(FirebaseEvent<Game> game)
		{
			if (!Games.Any(g => g.Key.Equals(game.Key)) && Games.Max(g => g.Object.Date) < game.Object.Date)
			{
				var games = Games.ToList();

				games.Insert(0, game);

				if (games.Count >= MAX_GAMES)
				{
					games.RemoveAt(MAX_GAMES);
				}

				UpdateGamePlayers(games.First());

				Games = new ObservableCollection<FirebaseObject<Game>>(games);
			}
		}

		private void RefreshGames(List<FirebaseObject<Game>> games)
		{
			foreach (var game in games)
			{
				UpdateGamePlayers(game);
			}

			Games = new ObservableCollection<FirebaseObject<Game>>(games);
		}

		private void UpdateGamePlayers(FirebaseObject<Game> game)
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
	}
}