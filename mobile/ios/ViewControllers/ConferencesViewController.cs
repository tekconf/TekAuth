using Foundation;
using System;
using UIKit;
using System.Linq;
using TekConf.Mobile.Core.ViewModel;

namespace ios
{
	partial class ConferencesViewController : UITableViewController
	{
		private const string cellId = "conferenceCell";

		public ConferencesViewController (IntPtr handle) : base (handle)
		{
		}

		private ConferencesViewModel Vm {
			get {
				return Application.Locator.Conferences;
			}
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return Vm.Conferences.Count ();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var conference = Vm.Conferences [indexPath.Row];
			ConferenceCell cell = this.TableView.DequeueReusableCell (cellId) as ConferenceCell;

			try {
				cell.SetConference (conference);

			} catch (Exception ex) {
				var sdsds = ex.Message;
			}
			return cell;
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (!Vm.Conferences.Any ()) {
				await Vm.LoadConferences ();
				this.TableView.ReloadData ();
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "showConferenceDetail") {
				var conference = Vm.Conferences [this.TableView.IndexPathForSelectedRow.Row];
				Application.Locator.Conference = new ConferenceDetailViewModel (conference);
			}
		}
	}
}