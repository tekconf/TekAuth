using Foundation;
using System;
using UIKit;
using System.Linq;
using TekConf.Mobile.Core.ViewModel;
using Xamarin;
using System.Collections.Generic;

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
				Insights.Report (ex);
			}
			return cell;
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Insights.Track("ViewedScreen", 
				new Dictionary <string,string> { 
					{"Screen", "Conferences"}, 
				});

			if (!Vm.Conferences.Any ()) {
				using (Insights.TrackTime ("Loading Conferences List")) {
					await Vm.LoadConferences ();
				}
				this.TableView.ReloadData ();
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "showConferenceDetail") {
				var conference = Vm.Conferences [this.TableView.IndexPathForSelectedRow.Row];
				Insights.Track ("UserSelectedConference", "ConferenceSlug", conference.Slug);
				Application.Locator.Conference = new ConferenceDetailViewModel (conference);
			}
		}
	}
}