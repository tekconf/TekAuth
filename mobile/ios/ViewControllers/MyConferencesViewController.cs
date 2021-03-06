using Foundation;
using System;
using UIKit;
using System.Linq;
using TekConf.Mobile.Core.ViewModels;
using Xamarin;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tekconf.DTO;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Fusillade;
using GalaSoft.MvvmLight.Messaging;
using TekConf.Mobile.Core.Messages;
using TekConf.Mobile.Core.Services;

namespace ios
{
	partial class MyConferencesViewController : UITableViewController, IUISearchResultsUpdating
	{
		private UIRefreshControl _uirc;
        private UISearchController _searchController;
        private ObservableCollection<Conference> _conferences;
        private ObservableCollection<Conference> _filteredConferences;

        private const string cellId = "conferenceCell";

		public MyConferencesViewController (IntPtr handle) : base (handle)
		{
			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 220;
        }

		private MyConferencesViewModel Vm {
			get {
				return Application.Locator.MyConferences;
			}
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return _filteredConferences.Count ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_conferences = Vm.MyConferences;
			_filteredConferences = _conferences;

			_uirc = new UIRefreshControl ();
			_uirc.ValueChanged += async (sender, e) => { 
				await LoadMyConferences (Priority.UserInitiated);

				_uirc.EndRefreshing ();
			};
			RefreshControl = _uirc;

            Messenger.Default.Register<ConferenceAddedToScheduleMessage>
            (
                this,
				async (action) => {
					_uirc.BeginRefreshing();
					await LoadMyConferences(Priority.UserInitiated);
					_uirc.EndRefreshing();
				}
            );

            Messenger.Default.Register<ConferenceRemovedFromScheduleMessage>
            (
                this,
				async (action) => {
					_uirc.BeginRefreshing();
					await LoadMyConferences(Priority.UserInitiated);
					_uirc.EndRefreshing();
				}
            );

            Messenger.Default.Register<AuthenticationInitializedMessage>
            (
                this,
				async (action) => {
					_uirc.BeginRefreshing();
					await LoadMyConferences(Priority.UserInitiated);
					_uirc.EndRefreshing();
				}
            );


		    _searchController = new UISearchController((UITableViewController) null)
		    {
		        DimsBackgroundDuringPresentation = false
		    };

		    this.TableView.TableHeaderView = _searchController.SearchBar;
			_searchController.SearchBar.SizeToFit ();
			DefinesPresentationContext = true;
			_searchController.SearchResultsUpdater = this;
			this.TableView.SetContentOffset (new CoreGraphics.CGPoint (0, _searchController.SearchBar.Frame.Size.Height), animated: false);
			_searchController.SearchBar.BarTintColor = UIColor.FromRGB (red: 34, green: 91, blue: 149);
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

		async Task LoadMyConferences (Priority priority)
		{
			using (Insights.TrackTime ("Loading MyConferences List")) {
				await Vm.LoadSchedules (priority);
				_conferences = Vm.MyConferences;
				_filteredConferences = _conferences;
			}
			this.TableView.ReloadData ();
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Insights.Track ("ViewedScreen", 
				new Dictionary <string,string> { 
					{ "Screen", "Conferences" }, 
				});

			if (!_filteredConferences.Any ()) {
				await LoadMyConferences (Priority.Background);
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "showConferenceDetail") {
				var conference = _filteredConferences [this.TableView.IndexPathForSelectedRow.Row];
				Insights.Track ("UserSelectedConference", "ConferenceSlug", conference.Slug);
				var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
				var schedulesService = ServiceLocator.Current.GetInstance<ISchedulesService>();
				Application.Locator.Conference = new ConferenceDetailViewModel (conference, schedulesService, settingsService);
				this.NavigationItem.BackBarButtonItem = new UIBarButtonItem ("Conferences", UIBarButtonItemStyle.Plain, null);

			}
		}

		public void SelectSession (string conferenceSlug, string sessionSlug)
		{
			if (string.IsNullOrWhiteSpace (conferenceSlug) || string.IsNullOrWhiteSpace(sessionSlug)) {
				return;
			}

			var row = _filteredConferences.ToList ().FindIndex (c => c.Slug == conferenceSlug);
			var indexPath = NSIndexPath.FromRowSection (row, 0);
			this.TableView.SelectRow (indexPath, animated: false, scrollPosition: UITableViewScrollPosition.Top);
			this.PerformSegue ("showConferenceDetail", this.TableView);
		}

		public void SelectConference (string conferenceSlug)
		{
			if (string.IsNullOrWhiteSpace (conferenceSlug)) {
				return;
			}

			var row = _filteredConferences.ToList ().FindIndex (c => c.Slug == conferenceSlug);
			var indexPath = NSIndexPath.FromRowSection (row, 0);
			this.TableView.SelectRow (indexPath, animated: false, scrollPosition: UITableViewScrollPosition.Top);
			this.PerformSegue ("showConferenceDetail", this.TableView);
		}

		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			var text = searchController.SearchBar.Text;
			if (searchController.Active) {
				var filteredList = _conferences.Where (p => 
					CultureInfo.CurrentCulture.CompareInfo.IndexOf
					(p.Name, text, CompareOptions.IgnoreCase) >= 0).ToList ();
				_filteredConferences = new ObservableCollection<Conference> (filteredList);
			} else {
				_filteredConferences = _conferences;
			}

			TableView.ReloadData ();
		}
	}
}