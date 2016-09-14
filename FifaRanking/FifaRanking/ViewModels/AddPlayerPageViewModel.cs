using System;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;
using Newtonsoft.Json;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class AddPlayerPageViewModel
	{
		public ICommand AddPlayerCommand { get; set; }

		public string Name { get; set; }

		public bool hasBack;

		public bool IsLoading { get; set; }

		public AddPlayerPageViewModel(bool hasBack)
		{
			AddPlayerCommand = new Command(AddPlayer);

			this.hasBack = hasBack;
		}

		private async void AddPlayer()
		{
			IsLoading = true;

			if (AreFieldsValid())
			{
				try
				{
					Player player = new Player();
					player.Name = Name;

					await App.Instance.RankingManager.AddPlayer(player);

					Name = "";
					await App.DisplayAlertAsync("Player added.");

					if (!hasBack)
					{
						await ((MainPage)App.Instance.MainPage).PopToRootAsync();
					}
				}
				catch
				{
					await App.DisplayAlertAsync("Add Player failed.");
				}
			}

			IsLoading = false;
		}

		private bool AreFieldsValid()
		{
			bool valid = !String.IsNullOrWhiteSpace(Name);
			if (!valid)
			{
				Device.BeginInvokeOnMainThread(async () => await App.DisplayAlertAsync("Please fill Name field."));
			}

			return valid;
		}
	}
}