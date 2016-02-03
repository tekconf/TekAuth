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
using CoreSpotlight;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core;
using Fusillade;
using GalaSoft.MvvmLight.Messaging;
using TekConf.Mobile.Core.Messages;
using TekConf.Mobile.Core.Services;
using CoreGraphics;
using ObjCRuntime;

namespace ios
{
	partial class ConferencesViewController : UITableViewController, IUISearchResultsUpdating
	{
		private UIRefreshControl _uirc;
        private ObservableCollection<Conference> _conferences;
        private ObservableCollection<Conference> _filteredConferences;
        private UISearchController _searchController;

        private const string cellId = "conferenceCell";

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
        
		UIView _emptyView;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_conferences = Vm.Conferences;
			_filteredConferences = _conferences;

			_uirc = new UIRefreshControl ();
			_uirc.ValueChanged += async (sender, e) => { 
				await LoadConferences (Priority.UserInitiated);
				_uirc.EndRefreshing ();
			};

			RefreshControl = _uirc;

			AddEmptyView ();
				
            Messenger.Default.Register<AuthenticationInitializedMessage>
            (
                this,
                async (message) => { 
					_uirc.BeginRefreshing();
					await LoadConferences(Priority.UserInitiated);
					_uirc.EndRefreshing();
				}
            );

			Messenger.Default.Register<ConferenceAddedMessage>
			(
				this,
				async (message) => { 
					TableView.SetContentOffset(new CoreGraphics.CGPoint(x:0, y: 0-_uirc.Frame.Size.Height-_searchController.SearchBar.Frame.Size.Height), animated:true);
					_uirc.BeginRefreshing();
					await LoadConferences(Priority.UserInitiated);
					_uirc.EndRefreshing();
					TableView.SetContentOffset(new CoreGraphics.CGPoint(x:0, y: -((_searchController.SearchBar.Frame.Size.Height - 10) * 2)), animated:true);

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

		private void AddEmptyView()
		{
//			var background = new UIView()  
//			{ 
//				BackgroundColor = UIColor.White 
//			};
//			var signInLabel = new UILabel (new CGRect(x:0, y:0, width:300, height:300)) {
//				Lines = 0,
//				LineBreakMode = UILineBreakMode.WordWrap
//			};
//			signInLabel.Font = UIFont.FromName("FontAwesome", 168f);
//			signInLabel.Text = "\xf090";
//			signInLabel.TextColor = UIColor.DarkGray;
//
//			background.AddSubview (signInLabel);

			var unauthenticatedView = Runtime.GetNSObject(NSBundle.MainBundle.LoadNib("UnauthenticatedView", this, null).ValueAt(0)) as UnauthenticatedView;

			TableView.BackgroundView = unauthenticatedView;
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

		async Task LoadConferences (Priority priority)
		{
			using (Insights.TrackTime ("Loading Conferences List")) {
				await Vm.LoadConferences (priority);
				_conferences = Vm.Conferences;
				_filteredConferences = _conferences;
				await PrepareForSearch (_conferences);
			}
			this.TableView.ReloadData ();
		}


		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(red: 34, green: 91, blue: 149);


			Insights.Track ("ViewedScreen", 
				new Dictionary <string,string> { 
					{ "Screen", "Conferences" }, 
				});

			if (!_filteredConferences.Any ()) {
				await LoadConferences (Priority.Background);
			}
		}

		async Task<CSSearchableItem> AddSessionToSearch (Conference conference, Session session)
		{
			
			var attributes = new CSSearchableItemAttributeSet (itemContentType: MobileCoreServices.UTType.DelimitedText.ToString());

			attributes.Title = session.Title;
			attributes.ContentDescription = session.Description;
			if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
				try {
					var imageService = ServiceLocator.Current.GetInstance<IImageService> ();
					var localPath = await imageService.GetImagePath (conference);
					UIImage image = null;
					await Task.Run (() => {
						var uiImage = UIImage.FromFile (localPath);
						if (uiImage != null) {
							attributes.ThumbnailData = uiImage.AsPNG ();
						}
					});
				} catch (Exception e) {
					Insights.Report (e);
				}
			}

			var searchableSession = new CSSearchableItem (conference.Slug + "|\\/|" + session.Slug, "tekconf", attributes);
			return searchableSession;
		}

		async Task<CSSearchableItem> AddConferenceToSearch (Conference conference)
		{
			var attributes = new CSSearchableItemAttributeSet (itemContentType: MobileCoreServices.UTType.DelimitedText.ToString());
			attributes.Title = conference.Name;
			attributes.ContentDescription = conference.Description;
			if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
				try {
					var imageService = ServiceLocator.Current.GetInstance<IImageService> ();
					var localPath = await imageService.GetImagePath (conference);
					UIImage image = null;
					await Task.Run (() => {
						var uiImage = UIImage.FromFile (localPath);
						if (uiImage != null) {
							attributes.ThumbnailData = uiImage.AsPNG ();
						}
					});
				} catch (Exception e) {
					Insights.Report (e);
				}
			}
			var searchableConference = new CSSearchableItem (conference.Slug, "tekconf", attributes);
			return searchableConference;
		}

		async Task PrepareForSearch (IEnumerable<Conference> conferences)
		{
			var conferencesList = conferences.ToList ();
			var searchableItems = new List<CSSearchableItem> ();

			for (int i = 0; i < conferencesList.Count (); i++) {
				var conference = conferencesList.ToArray () [i];

				var searchableConference = await AddConferenceToSearch (conference);
				searchableItems.Add (searchableConference);

				foreach (var session in conference.Sessions) {
					var searchableSession = await AddSessionToSearch (conference, session);
					searchableItems.Add (searchableSession);
				}
			}

			try {
				var searchableItemsArray = searchableItems.ToArray ();
				CSSearchableIndex.DefaultSearchableIndex.Index (searchableItemsArray, (error) => {
					// Successful?
					if (error != null) {
						Console.WriteLine (error.LocalizedDescription);
					}
				});
			} catch (Exception ex) {
				var xx = ex.Message;
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