using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

using Firebase.Xamarin.Database;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class PlayersPageViewModel
	{
		public ICommand AddPlayersCommand { get; set; }

		public ICommand ShowPlayerCommand { get; set; }

		public bool IsLoading { get; set; }

		public ObservableCollection<FirebaseObject<Player>> Players { get; set; }

		public PlayersPageViewModel()
		{
			AddPlayersCommand = new Command(AddPlayers);

			ShowPlayerCommand = new Command<FirebaseObject<Player>>(ShowPlayer);
		}

		public async void LoadPlayers()
		{
			IsLoading = true;

			var players = await App.Instance.RankingManager.GetPlayers();

			Players = new ObservableCollection<FirebaseObject<Player>>(players);

			IsLoading = false;
		}

		private async void AddPlayers()
		{
			await ((MainPage)App.Instance.MainPage).PushAsync(new AddPlayerPage());
		}

		public async void ShowPlayer(FirebaseObject<Player> player)
		{
			await ((MainPage)App.Instance.MainPage).PushAsync(new PlayerPage(player));
		}
	}
}