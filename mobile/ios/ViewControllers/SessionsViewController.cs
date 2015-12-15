using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModel;

namespace ios
{
	partial class SessionsViewController : UITableViewController, IUISearchResultsUpdating
	{
		List<Session> Sessions = new List<Session> () { 
			new Session { 
				Title = "Let’s Build a Hybrid Mobile App!",
				Description = "Let’s Build a Hybrid Mobile App!",
				Id = 1,
				Room = "Ballroom A",
				SpeakerName = "Rob Gibbens",
				StartDate = new DateTime(2016, 04, 16, 13, 0,0),
				EndDate = new DateTime(2016, 04, 16, 14, 0,0),

			}, 
			new Session { 
				Title = "Let’s Build a Hybrid Mobile App!",
				Description = "Let’s Build a Hybrid Mobile App!",
				Id = 1,
				Room = "Ballroom A",
				SpeakerName = "Rob Gibbens",
				StartDate = new DateTime(2016, 04, 16, 13, 0,0),
				EndDate = new DateTime(2016, 04, 16, 14, 0,0),
			}, 
		};
		List<Session> FilteredSessions = new List<Session>();

		public SessionsViewController (IntPtr handle) : base (handle)
		{
			FilteredSessions = Sessions;
		}
		UISearchController searchController;
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

		private ConferenceDetailViewModel Vm {
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
			var session = FilteredSessions.ToArray()[indexPath.Row];
			cell.SetSession (session);
			//cell.TextLabel.Text = FilteredSessions.ToArray () [indexPath.Row];

			return cell;

		}


		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			var text = searchController.SearchBar.Text;
			if (searchController.Active) {
				FilteredSessions = Sessions.Where (x => x.Title.Contains (text)).ToList ();
			} else {
				FilteredSessions = Sessions;
			}

			TableView.ReloadData ();
		}
	}
}
