using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

using Firebase.Xamarin.Database;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class PlayerPageViewModel
	{
		public Player Player { get; set; }

		public PlayerPageViewModel(FirebaseObject<Player> player)
		{
			Player = player.Object;
		}
	}
}