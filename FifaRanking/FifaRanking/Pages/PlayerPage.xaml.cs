using System;

using Xamarin.Forms;

using Firebase.Xamarin.Database;

namespace FifaRanking
{
	public partial class PlayerPage : ContentPage
	{
		public PlayerPage(FirebaseObject<Player> player)
		{
			InitializeComponent();

			BindingContext = new PlayerPageViewModel(player);
		}
	}
}