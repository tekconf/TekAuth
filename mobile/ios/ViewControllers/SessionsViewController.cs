using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModels;
using Xamarin;
using System.Globalization;

namespace ios
{
	partial class SessionsViewController : UITableViewController, IUISearchResultsUpdating
	{
		List<Session> FilteredSessions = new List<Session> ();

		public SessionsViewController (IntPtr handle) : base (handle)
		{
			FilteredSessions = Vm.Conference.Sessions;
			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 212;
		}

		UISearchController searchController;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.NavigationBar.BarTintColor = UIColorExtensions.FromHex (Application.Locator.Conference.Conference.HighlightColor);

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			searchController = new UISearchController ((UITableViewController)null);
			searchController.DimsBackgroundDuringPresentation = false;
			this.TableView.TableHeaderView = searchController.SearchBar;
			searchController.SearchBar.SizeToFit ();
			DefinesPresentationContext = true;
			searchController.SearchResultsUpdater = this;
			this.TableView.SetContentOffset (new CoreGraphics.CGPoint (0, searchController.SearchBar.Frame.Size.Height), animated: false);
			searchController.SearchBar.BarTintColor = UIColorExtensions.FromHex (Vm.Conference.HighlightColor);
		}

		private ConferenceDetailViewModel2 Vm {
			get {
				return Application.Locator.Conference;
			}
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return FilteredSessions.Count ();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("sessionCell") as SessionCell;
			var session = FilteredSessions.ToArray () [indexPath.Row];
			cell.SetSession (session, this.Vm.Conference.HighlightColor);

			return cell;
		}

		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			var text = searchController.SearchBar.Text;
			if (searchController.Active) {
				FilteredSessions = Vm
				.Conference
				.Sessions
				.Where (
					x => CultureInfo.CurrentCulture.CompareInfo.IndexOf (x.Title, text, CompareOptions.IgnoreCase) >= 0
					|| x.Speakers.Any (s => CultureInfo.CurrentCulture.CompareInfo.IndexOf (s.LastName, text, CompareOptions.IgnoreCase) >= 0)
				)
				.ToList ();
			} else {
				FilteredSessions = Vm.Conference.Sessions;
			}

			TableView.ReloadData ();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.Identifier == "showSessionDetail") {
				var session = Vm.Conference.Sessions [this.TableView.IndexPathForSelectedRow.Row];
				Insights.Track ("UserSelectedSession", "SessionSlug", session.Slug);

				Application.Locator.Session = new SessionDetailViewModel (session, Vm.Conference.Name);

				this.NavigationItem.BackBarButtonItem = new UIBarButtonItem ("Sessions", UIBarButtonItemStyle.Plain, null);

			}
		}
	}
}