using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FifaRanking
{
	public class MainPage : MasterDetailPage
	{
		private NavigationPage navPage;

		public MainPage()
		{
			navPage = new NavigationPage(new HomePage())
				{
					BarTextColor = Color.White,
					BarBackgroundColor = Color.Green
				};

			Master = new SideMenuPage();
			Detail = navPage;

			MasterBehavior = MasterBehavior.Popover;
		}

		public async Task PushAsync(Page page, bool animated = true)
		{
			await navPage.PushAsync(page, animated);
		}

		public async Task PushWithPopToRootAsync(Page page)
		{
			IsPresented = false;
			await navPage.PopToRootAsync(false);
			await navPage.PushAsync(page);
		}

		public async Task PopAsync()
		{
			await navPage.PopAsync();
		}

		public async Task PopToRootAsync()
		{
			await navPage.PopToRootAsync();
		}
	}
}