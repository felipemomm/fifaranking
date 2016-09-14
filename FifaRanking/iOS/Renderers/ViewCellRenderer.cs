using UIKit;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ViewCell), typeof(FifaRanking.iOS.ViewCellRenderer))]
namespace FifaRanking.iOS
{
	public class ViewCellRenderer : Xamarin.Forms.Platform.iOS.ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			UITableViewCell cell = base.GetCell(item, reusableCell, tv);

			if (cell != null) 
			{
				cell.LayoutMargins = UIEdgeInsets.Zero;
				cell.SeparatorInset = new UIEdgeInsets(0, 0, 0, 0);
			}

			return cell;
		}
	}
}