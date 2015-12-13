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
		List<string> Sessions = new List<string> () { "Let’s Build a Hybrid Mobile App!", "Cross Platform Mobile UI with Xamarin Forms Workshop", "Game Development with the Unity Game Engine – Part 1", "Breaking Ground with iOS Development – Part 1", "XAML & C# Powered iOS, Android, and Windows apps"};
		List<string> FilteredSessions = new List<string>();

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
			var cell = tableView.DequeueReusableCell ("sessionCell");
			cell.TextLabel.Text = FilteredSessions.ToArray () [indexPath.Row];

			return cell;

		}


		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			var text = searchController.SearchBar.Text;
			if (searchController.Active) {
				FilteredSessions = Sessions.Where (x => x.Contains (text)).ToList ();
			} else {
				FilteredSessions = Sessions;
			}

			TableView.ReloadData ();
		}
	}
}
