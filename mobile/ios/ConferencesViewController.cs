using Foundation;
using System;
using UIKit;
using System.Linq;
using CoreGraphics;
namespace ios
{
	partial class ConferencesViewController : UITableViewController
	{
		public ConferencesViewController (IntPtr handle) : base (handle)
		{
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return AppDelegate.Conferences.Count ();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			string name = AppDelegate.Conferences [indexPath.Row].Name;
			var cell = new UITableViewCell (CGRect.Empty);
			cell.TextLabel.Text = name;
			return cell;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.TableView.ReloadData ();
		}
	}
}