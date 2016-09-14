using System;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Forms;

namespace FifaRanking
{
	[ImplementPropertyChanged]
	public class LoginPageViewModel
	{
		public ICommand LoginCommand { get; set; }

		public ICommand CreateCommand { get; set; }

		public string User { get; set; }

		public string Password { get; set; }

		public bool IsLoading { get; set; }

		public LoginPageViewModel()
		{
			LoginCommand = new Command(Login);
			CreateCommand = new Command(Create);

			User = App.Settings.GetValueOrDefault(Constants.USER_EMAIL, "");
		}

		private async void Login()
		{
			IsLoading = true;

			if (AreFieldsValid())
			{
				try
				{
					await App.Instance.AuthManager.Login(User, Password);

					App.Settings.AddOrUpdateValue(Constants.USER_EMAIL, User);

					await ((MainPage)App.Instance.MainPage).PopToRootAsync();
				}
				catch
				{
					await App.DisplayAlertAsync("Login failed.");
				}
			}

			IsLoading = false;
		}

		private async void Create()
		{
			IsLoading = true;

			if (AreFieldsValid())
			{
				try
				{
					await App.Instance.AuthManager.Create(User, Password);

					App.Settings.AddOrUpdateValue(Constants.USER_EMAIL, User);

					await ((MainPage)App.Instance.MainPage).PushAsync(new AddPlayerPage(false));
				}
				catch
				{
					await App.DisplayAlertAsync("User creation failed.");
				}
			}

			IsLoading = false;
		}

		private bool AreFieldsValid()
		{
			bool valid = !String.IsNullOrWhiteSpace(User) && !String.IsNullOrWhiteSpace(Password) && User.EndsWith("@arctouch.com");
			if (!valid)
			{
				Device.BeginInvokeOnMainThread(async () => await App.DisplayAlertAsync("Please fill User and Password fields."));
			}

			return valid;
		}
	}
}