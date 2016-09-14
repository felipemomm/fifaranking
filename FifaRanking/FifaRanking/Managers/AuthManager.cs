using System;
using System.Threading.Tasks;

using Firebase.Xamarin.Auth;

namespace FifaRanking
{
	public class AuthManager
	{
		public FirebaseAuthLink Auth { get; set; }

		public bool IsAuthenticated 
		{ 
			get 
			{
				return this.Auth != null;
			}
		}

		public async Task Create(string email, string password)
		{
			FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.FIREBASE_APPKEY));

			this.Auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

			// The auth Object will contain auth.User and the Authentication Token from the request
			System.Diagnostics.Debug.WriteLine(this.Auth.FirebaseToken);
		}

		public async Task Login(string email, string password)
		{
			FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.FIREBASE_APPKEY));

			this.Auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

			// The auth Object will contain auth.User and the Authentication Token from the request
			System.Diagnostics.Debug.WriteLine(this.Auth.FirebaseToken);
		}

		public void Logout()
		{
			this.Auth = null;
		}
	}
}