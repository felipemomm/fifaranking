using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace FifaRanking
{
	public class RankingManager
	{
		public async Task AddPlayer(Player player)
		{
			FirebaseClient firebase = new FirebaseClient(Constants.FIREBASE_URL);

			await firebase
				.Child("players")
				//.WithAuth(App.Instance.AuthManager.Auth.FirebaseToken)
				.PostAsync(player);
		}

		public async Task AddGame(Game game)
		{
			FirebaseClient firebase = new FirebaseClient(Constants.FIREBASE_URL);

			await firebase
				.Child("games")
				//.WithAuth(App.Instance.AuthManager.Auth.FirebaseToken)
				.PostAsync(game);
		}

		public async Task<List<FirebaseObject<Player>>> GetPlayers()
		{
			FirebaseClient firebase = new FirebaseClient(Constants.FIREBASE_URL);

			var items = await firebase
				.Child("players")
				//.WithAuth(App.Instance.AuthManager.Auth.FirebaseToken)
				.OnceAsync<Player>();

			return items.OrderBy(i => i.Object.Name).ToList();
		}

		public async Task<List<FirebaseObject<Player>>> GetRankedPlayers()
		{
			FirebaseClient firebase = new FirebaseClient(Constants.FIREBASE_URL);

			var items = await firebase
				.Child("players")
				//.WithAuth(App.Instance.AuthManager.Auth.FirebaseToken)
				.OnceAsync<Player>();

			return items.Where(i => i.Object.Ranking != null).OrderBy(i => i.Object.Ranking).ToList();
		}

		public async Task<List<FirebaseObject<Game>>> GetLatestGames()
		{
			FirebaseClient firebase = new FirebaseClient(Constants.FIREBASE_URL);

			var items = await firebase
				.Child("games")
	            //.WithAuth(App.Instance.AuthManager.Auth.FirebaseToken)
				.OnceAsync<Game>();

			return items.OrderByDescending(i => i.Object.Date).Take(10).ToList();
		}

		public async Task UpdatePlayers(List<FirebaseObject<Player>> players)
		{
			FirebaseClient firebase = new FirebaseClient(Constants.FIREBASE_URL);

			foreach (var player in players.Where(p => p.Object.HasChanged))
			{
				await firebase
					.Child("players")
					.Child(player.Key)
					//.WithAuth(App.Instance.AuthManager.Auth.FirebaseToken)
					.PutAsync<Player>(player.Object);
			}
		}

		public void UpdatePlayerStats(Game game, List<FirebaseObject<Player>> players)
		{
			if (game.Team1Goals != game.Team2Goals)
			{
				bool team1won = game.Team1Goals > game.Team2Goals;

				FirebaseObject<Player> winner1 = players.FirstOrDefault(p => p.Key.Equals(team1won ? game.Team1Player1 : game.Team2Player1));
				FirebaseObject<Player> winner2 = players.FirstOrDefault(p => p.Key.Equals(team1won ? game.Team1Player2 : game.Team2Player2));
				FirebaseObject<Player> loser1 = players.FirstOrDefault(p => p.Key.Equals(team1won ? game.Team2Player1 : game.Team1Player1));
				FirebaseObject<Player> loser2 = players.FirstOrDefault(p => p.Key.Equals(team1won ? game.Team2Player2 : game.Team1Player2));

				if (team1won)
				{
					UpdateWinnerGoalsAndWins(winner1, game.Team1Goals, game.Team2Goals);
					UpdateWinnerGoalsAndWins(winner2, game.Team1Goals, game.Team2Goals);

					UpdateLoserGoalsAndWins(loser1, game.Team2Goals, game.Team1Goals);
					UpdateLoserGoalsAndWins(loser2, game.Team2Goals, game.Team1Goals);
				}
				else if (!team1won)
				{
					UpdateWinnerGoalsAndWins(winner1, game.Team2Goals, game.Team1Goals);
					UpdateWinnerGoalsAndWins(winner2, game.Team2Goals, game.Team1Goals);

					UpdateLoserGoalsAndWins(loser1, game.Team1Goals, game.Team2Goals);
					UpdateLoserGoalsAndWins(loser2, game.Team1Goals, game.Team2Goals);
				}

				AddUnrankedPlayerToRanking(winner1, players);
				AddUnrankedPlayerToRanking(winner2, players);

				UpdateWinnersRanking(winner1, winner2, loser1, loser2, players);
			}
			else
			{
				foreach (var player in players.Where(p => p.Key.Equals(game.Team1Player1)
					|| p.Key.Equals(game.Team1Player2) || p.Key.Equals(game.Team2Player1) || p.Key.Equals(game.Team2Player2)))
				{
					player.Object.Draws = player.Object.Draws + 1;
					player.Object.GoalsScored = player.Object.GoalsScored + game.Team1Goals;
					player.Object.GoalsAgainst = player.Object.GoalsAgainst + game.Team1Goals;
					player.Object.HasChanged = true;

					AddUnrankedPlayerToRanking(player, players);
				}
			}
		}

		private void UpdateWinnerGoalsAndWins(FirebaseObject<Player> winner, int goalsScored, int goalsAgainst)
		{
			winner.Object.Wins = winner.Object.Wins + 1;
			winner.Object.GoalsScored = winner.Object.GoalsScored + goalsScored;
			winner.Object.GoalsAgainst = winner.Object.GoalsAgainst + goalsAgainst;
			winner.Object.StraightWins = winner.Object.StraightWins + 1;

			if (winner.Object.MaxStraightWins < winner.Object.StraightWins)
			{
				winner.Object.MaxStraightWins = winner.Object.StraightWins;
			}

			winner.Object.HasChanged = true;
		}

		private void UpdateLoserGoalsAndWins(FirebaseObject<Player> loser, int goalsScored, int goalsAgainst)
		{
			loser.Object.Loses = loser.Object.Loses + 1;
			loser.Object.GoalsScored = loser.Object.GoalsScored + goalsScored;
			loser.Object.GoalsAgainst = loser.Object.GoalsAgainst + goalsAgainst;
			loser.Object.StraightWins = 0;

			loser.Object.HasChanged = true;
		}

		private void AddUnrankedPlayerToRanking(FirebaseObject<Player> player, List<FirebaseObject<Player>> players)
		{
			if (player.Object.Ranking == null)
			{
				var lastRankedPlayer = players.Where(p => p.Object.Ranking != null).OrderByDescending(p => p.Object.Ranking).FirstOrDefault();

				if (lastRankedPlayer == null)
				{
					player.Object.Ranking = 1;
				}
				else
				{
					player.Object.Ranking = (lastRankedPlayer.Object.Ranking ?? 0) + 1;
				}

				player.Object.BestRanking = player.Object.Ranking;
			}
		}

		private void UpdateWinnersRanking(FirebaseObject<Player> winner1, FirebaseObject<Player> winner2, FirebaseObject<Player> loser1, FirebaseObject<Player> loser2, List<FirebaseObject<Player>> players)
		{
			int bestLoserRanking = -1;
			if (loser1.Object.Ranking != null && loser2.Object.Ranking != null)
			{
				bestLoserRanking = Math.Min((int)loser1.Object.Ranking, (int)loser2.Object.Ranking);
			}
			else if (loser1.Object.Ranking != null)
			{
				bestLoserRanking = (int)loser1.Object.Ranking;
			}
			else if (loser2.Object.Ranking != null)
			{
				bestLoserRanking = (int)loser2.Object.Ranking;
			}

			var bestWinner = winner1.Object.Ranking < winner2.Object.Ranking ? winner1 : winner2;
			var worstWinner = winner1.Object.Ranking > winner2.Object.Ranking ? winner1 : winner2;

			if (bestWinner.Object.Ranking > bestLoserRanking && bestLoserRanking > -1) // If at least one loser is ranked
			{
				int newRanking = (int)Math.Floor((double)((int)bestWinner.Object.Ranking + bestLoserRanking) / 2);

				foreach (var player in players.Where(p => p.Object.Ranking != null && p.Object.Ranking < bestWinner.Object.Ranking 
					&& p.Object.Ranking >= newRanking))
				{
					player.Object.Ranking = player.Object.Ranking + 1;
					player.Object.HasChanged = true;
				}

				bestWinner.Object.Ranking = newRanking;

				if (bestWinner.Object.BestRanking > bestWinner.Object.Ranking)
				{
					bestWinner.Object.BestRanking = bestWinner.Object.Ranking;
				}
			}

			if (worstWinner.Object.Ranking > bestLoserRanking && bestLoserRanking > 0)
			{
				int newRanking = (int)Math.Floor((double)((int)worstWinner.Object.Ranking + bestLoserRanking) / 2);

				foreach (var player in players.Where(p => p.Object.Ranking != null && p.Object.Ranking < worstWinner.Object.Ranking 
					&& p.Object.Ranking >= newRanking && !p.Key.Equals(bestWinner.Key)))
				{
					player.Object.Ranking = player.Object.Ranking + 1;
					player.Object.HasChanged = true;
				}

				// If in the end, both winners have the same rank, the best one will end up with the best rank
				worstWinner.Object.Ranking = (bestWinner.Object.Ranking != newRanking) ? newRanking : newRanking + 1;

				if (worstWinner.Object.BestRanking > worstWinner.Object.Ranking)
				{
					worstWinner.Object.BestRanking = worstWinner.Object.Ranking;
				}
			}
		}
	}
}