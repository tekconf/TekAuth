using Foundation;
using System;
using UIKit;
using System.Linq;
using TekConf.Mobile.Core.ViewModel;
using Xamarin;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tekconf.DTO;
using System.Globalization;

namespace ios
{
	partial class ConferencesViewController : UITableViewController, IUISearchResultsUpdating
	{
		private const string cellId = "conferenceCell";

		private ObservableCollection<Conference> _conferences;
		private ObservableCollection<Conference> _filteredConferences;
		public ConferencesViewController (IntPtr handle) : base (handle)
		{
			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 220;
		}

		private ConferencesViewModel Vm {
			get {
				return Application.Locator.Conferences;
			}
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return _filteredConferences.Count ();
		}

		UISearchController searchController;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_conferences = Vm.Conferences;
			_filteredConferences = _conferences;

			searchController = new UISearchController ((UITableViewController)null);
			searchController.DimsBackgroundDuringPresentation = false;
			this.TableView.TableHeaderView = searchController.SearchBar;
			searchController.SearchBar.SizeToFit ();
			DefinesPresentationContext = true;
			searchController.SearchResultsUpdater = this;
			this.TableView.SetContentOffset (new CoreGraphics.CGPoint (0, searchController.SearchBar.Frame.Size.Height), animated: false);
			searchController.SearchBar.BarTintColor = UIColor.FromRGB(red: 34, green: 91, blue: 149);

		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var conference = _filteredConferences [indexPath.Row];
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

			if (!_filteredConferences.Any ()) {
				using (Insights.TrackTime ("Loading Conferences List")) {
					await Vm.LoadConferences ();
					_conferences = Vm.Conferences;
					_filteredConferences = _conferences;
				}
				this.TableView.ReloadData ();
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "showConferenceDetail") {
				var conference = _filteredConferences [this.TableView.IndexPathForSelectedRow.Row];
				Insights.Track ("UserSelectedConference", "ConferenceSlug", conference.Slug);
				Application.Locator.Conference = new ConferenceDetailViewModel (conference);
			}
		}

		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			var text = searchController.SearchBar.Text;
			if (searchController.Active) {
				var filteredList =  _conferences.Where(p => 
					CultureInfo.CurrentCulture.CompareInfo.IndexOf
					(p.Name, text, CompareOptions.IgnoreCase) >= 0).ToList();
				_filteredConferences = new ObservableCollection<Conference>(filteredList);
			} else {
				_filteredConferences = _conferences;
			}

			TableView.ReloadData ();
		}
	}
}