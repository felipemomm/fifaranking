using System;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(FifaRanking.Droid.NavigationRenderer))]
namespace FifaRanking.Droid
{
	public class NavigationRenderer : Xamarin.Forms.Platform.Android.NavigationRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
		{
			base.OnElementChanged(e);

			RemoveAppIconFromActionBar();
		}

		private void RemoveAppIconFromActionBar()
		{
			ActionBar actionBar = ((Activity)Context).ActionBar;
			actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));
		}
	}
}