using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

using Firebase.Xamarin.Database;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class RankingPageViewModel
	{
		public ICommand ShowPlayerCommand { get; set; }

		public bool IsLoading { get; set; }

		public ObservableCollection<FirebaseObject<Player>> Players { get; set; }

		private bool firstAppearance = true;

		public RankingPageViewModel()
		{
			ShowPlayerCommand = new Command<FirebaseObject<Player>>(ShowPlayer);
		}

		public async void LoadPlayers()
		{
			if (firstAppearance)
			{
				IsLoading = true;

				var players = await App.Instance.RankingManager.GetRankedPlayers();

				Players = new ObservableCollection<FirebaseObject<Player>>(players);

				IsLoading = false;

				firstAppearance = false;
			}
		}

		public async void ShowPlayer(FirebaseObject<Player> player)
		{
			await ((MainPage)App.Instance.MainPage).PushAsync(new PlayerPage(player));
		}
	}
}