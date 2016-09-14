using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UIKit;

[assembly: ExportRenderer(typeof(ListView), typeof(FifaRanking.iOS.ListViewRenderer))]
namespace FifaRanking.iOS
{
	public class ListViewRenderer : Xamarin.Forms.Platform.iOS.ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if (this.Control == null || e.NewElement == null)
			{
				return;
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				this.Control.LayoutMargins = UIEdgeInsets.Zero;
			}

			this.Control.TableFooterView = new UIView();
		}
	}
}