using Foundation;
using System;
using UIKit;
using System.Linq;
using CoreGraphics;
namespace ios
{
	partial class ConferencesViewController : UITableViewController
	{
		private const string cellId = "conferenceCell";

		public ConferencesViewController (IntPtr handle) : base (handle)
		{
			//TableView.RowHeight = UITableView.AutomaticDimension;
			//TableView.EstimatedRowHeight = 220;

		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return AppDelegate.Conferences.Count ();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var conference = AppDelegate.Conferences [indexPath.Row];
			ConferenceCell cell = this.TableView.DequeueReusableCell (cellId) as ConferenceCell;

			cell.SetConference (conference);
			return cell;
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);


			this.TableView.ReloadData ();
		}
	}
}