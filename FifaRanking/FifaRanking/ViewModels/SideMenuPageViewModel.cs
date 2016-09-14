using System;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class SideMenuPageViewModel
	{
		public ICommand TapMenuItemCommand { get; set; }

		public SideMenuPageViewModel()
		{
			TapMenuItemCommand = new Command<MenuItem>(TapMenuItem);
		}

		private async void TapMenuItem(MenuItem menuItem)
		{
			switch (menuItem)
			{
				case MenuItem.Ranking:
					await ((MainPage)App.Instance.MainPage).PushWithPopToRootAsync(new RankingPage());
					break;

				case MenuItem.Players:
					await ((MainPage)App.Instance.MainPage).PushWithPopToRootAsync(new PlayersPage());
					break;

				case MenuItem.LatestGames:
					await ((MainPage)App.Instance.MainPage).PushWithPopToRootAsync(new LatestGamesPage());
					break;

				case MenuItem.AddGame:
					await ((MainPage)App.Instance.MainPage).PushWithPopToRootAsync(new AddGamePage());
					break;

				case MenuItem.Logout:
					App.Instance.AuthManager.Logout();
					await ((MainPage)App.Instance.MainPage).PushWithPopToRootAsync(new LoginPage());
					break;

				default:
					break;
			}
		}
	}
}