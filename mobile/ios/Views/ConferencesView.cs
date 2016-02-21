using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using TekConf.Mobile.Core.ViewModels;
using System.Collections.Generic;
using MvvmCross.Binding.iOS.Views;
using Foundation;
using UIKit;
using Tekconf.DTO;
using System;
using System.Collections.ObjectModel;
using CoreGraphics;
using System.Threading.Tasks;
using CoreSpotlight;
using Xamarin;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using TekConf.Mobile.Core.Services;
using System.Globalization;
using GalaSoft.MvvmLight.Messaging;
using TekConf.Mobile.Core.Messages;

namespace ios.Views
{
	public class ConferencesView : MvxTableViewController<ConferencesViewModel>, IUISearchResultsUpdating
	{
		private UIRefreshControl _uirc;
		private UISearchController _searchController;
		private ObservableCollection<Conference> _filteredConferences;

		public ConferencesView ()
		{
			
			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 221;
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Conferences";
			this.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			AddSettingsButton ();
			AddFilterButton ();
			AddRefreshControl ();

			LoadTable ();

			ListenForMessages ();

			AddSearchController();
		}

		void LoadTable ()
		{
			try {
				//var source = new MvxSimpleTableViewSource (TableView, ConfListCell.Key, ConfListCell.Key);
				var source = new MvxStandardTableViewSource(TableView, "TitleText Name;");
				TableView.Source = source;
				var set = this.CreateBindingSet<ConferencesView, ConferencesViewModel> ();
				set.Bind (source).To (vm => vm.Conferences);
				set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ShowConference);
				set.Apply ();
				TableView.ReloadData ();

				this.ViewModel.LoadConferences.Execute ();
			} catch (Exception ex) {
				var sdsd = ex.Message;
				Console.WriteLine ("ERROR!!! - " + sdsd);
			}

		}

		void AddRefreshControl ()
		{
			_uirc = new UIRefreshControl ();
			_uirc.ValueChanged += (sender, e) => {
				this.ViewModel.LoadConferences.Execute ();
				_uirc.EndRefreshing ();
			};
			RefreshControl = _uirc;
		}

		void AddSearchController ()
		{
			_searchController = new UISearchController ((UITableViewController)null) {
				DimsBackgroundDuringPresentation = false
			};
			this.TableView.TableHeaderView = _searchController.SearchBar;
			_searchController.SearchBar.SizeToFit ();
			DefinesPresentationContext = true;
			_searchController.SearchResultsUpdater = this;
			this.TableView.SetContentOffset (new CGPoint (0, _searchController.SearchBar.Frame.Size.Height), animated: false);
			_searchController.SearchBar.BarTintColor = UIColor.FromRGB (red: 128, green: 153, blue: 77);
		}

		private void AddFilterButton ()
		{
			var filterAttributes = new UIStringAttributes () {
				ForegroundColor = UIColor.White,
				Font = UIFont.FromName ("FontAwesome", 16f)
			};

			UIButton menuButton = new UIButton (UIButtonType.Custom);
			var prettyString = new NSMutableAttributedString ("\xf0b0");
			prettyString.SetAttributes (filterAttributes.Dictionary, new NSRange (0, 1));
			menuButton.SetAttributedTitle (prettyString, UIControlState.Normal);
			menuButton.Frame = new CGRect (0, 0, 24, 24);

			UIBarButtonItem menuItem = new UIBarButtonItem (menuButton);

			menuButton.TouchUpInside += (sender, e) => {
				var storyboard = UIStoryboard.FromName ("Main", null);
				var filterViewController = storyboard.InstantiateViewController ("ConferencesFilterNavigationController") as ConferencesFilterNavigationController;

				this.NavigationController.PresentModalViewController (filterViewController, animated: true);
			};

			this.NavigationItem.SetLeftBarButtonItem (menuItem, true);

		}

		private void AddSettingsButton ()
		{
			var settingsAttributes = new UIStringAttributes () {
				ForegroundColor = UIColor.White,
				Font = UIFont.FromName ("FontAwesome", 16f)
			};

			UIButton menuButton = new UIButton (UIButtonType.Custom);
			var prettyString = new NSMutableAttributedString ("\xf013");
			prettyString.SetAttributes (settingsAttributes.Dictionary, new NSRange (0, 1));
			menuButton.SetAttributedTitle (prettyString, UIControlState.Normal);
			menuButton.Frame = new CGRect (0, 0, 24, 24);

			UIBarButtonItem menuItem = new UIBarButtonItem (menuButton);

			menuButton.TouchUpInside += (sender, e) => {
				var storyboard = UIStoryboard.FromName ("Main", null);
				var settingsController = storyboard.InstantiateViewController ("SettingsNavigationController") as SettingsNavigationController;

				this.NavigationController.PresentModalViewController (settingsController, animated: true);

			};

			this.NavigationItem.SetRightBarButtonItem (menuItem, true);

		}

		void ListenForMessages ()
		{
			Messenger.Default.Register<AuthenticationInitializedMessage> (this, async message => {
				_uirc.BeginRefreshing ();
				//await LoadConferences (Priority.UserInitiated);
				_uirc.EndRefreshing ();
			});
			Messenger.Default.Register<ConferenceAddedMessage> (this, async message => {
				TableView.SetContentOffset (new CoreGraphics.CGPoint (x: 0, y: 0 - _uirc.Frame.Size.Height - _searchController.SearchBar.Frame.Size.Height), animated: true);
				_uirc.BeginRefreshing ();
				//await LoadConferences (Priority.UserInitiated);
				_uirc.EndRefreshing ();
				TableView.SetContentOffset (new CoreGraphics.CGPoint (x: 0, y: -((_searchController.SearchBar.Frame.Size.Height - 10) * 2)), animated: true);
			});
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
					foreach (var speaker in session.Speakers) {
						var searchableSpeaker = await AddSpeakerToSearch (conference, session, speaker);
						searchableItems.Add (searchableSpeaker);
					}
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

		async Task<CSSearchableItem> AddSessionToSearch (Conference conference, Session session)
		{

			var attributes = new CSSearchableItemAttributeSet (itemContentType: MobileCoreServices.UTType.DelimitedText.ToString ());

			attributes.Title = session.Title;
			attributes.ContentDescription = session.Description;
			if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
				try {
					var imageService = ServiceLocator.Current.GetInstance<IImageService> ();
					var localPath = await imageService.GetConferenceImagePath (conference);
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

		async Task<CSSearchableItem> AddSpeakerToSearch (Conference conference, Session session, Speaker speaker)
		{

			var attributes = new CSSearchableItemAttributeSet (itemContentType: MobileCoreServices.UTType.DelimitedText.ToString ());

			attributes.Title = speaker.FirstName + " " + speaker.LastName + " - " + conference.Name;
			attributes.ContentDescription = speaker.Bio;
			if (!string.IsNullOrWhiteSpace (speaker.ImageUrl)) {
				try {
					var imageService = ServiceLocator.Current.GetInstance<IImageService> ();
					var localPath = await imageService.GetSpeakerImagePath (conference, speaker);
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

			var searchableSession = new CSSearchableItem (conference.Slug + "|\\/|" + session.Slug + "|\\/|" + speaker.Slug, "tekconf", attributes);
			return searchableSession;
		}

		async Task<CSSearchableItem> AddConferenceToSearch (Conference conference)
		{
			var attributes = new CSSearchableItemAttributeSet (itemContentType: MobileCoreServices.UTType.DelimitedText.ToString ());
			attributes.Title = conference.Name;
			attributes.ContentDescription = conference.Description;
			if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
				try {
					var imageService = ServiceLocator.Current.GetInstance<IImageService> ();
					var localPath = await imageService.GetConferenceImagePath (conference);
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

		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			var text = searchController.SearchBar.Text;
			if (searchController.Active) {
				var filteredList = this.ViewModel.Conferences.Where (
					                   conf => CultureInfo.CurrentCulture.CompareInfo.IndexOf (conf.Name, text, CompareOptions.IgnoreCase) >= 0
					                   || conf.Sessions.Any (session => CultureInfo.CurrentCulture.CompareInfo.IndexOf (session.Title, text, CompareOptions.IgnoreCase) >= 0
					                   || conf.Sessions.Any (session2 => session2.Speakers.Any (speaker => CultureInfo.CurrentCulture.CompareInfo.IndexOf (speaker.LastName, text, CompareOptions.IgnoreCase) >= 0)) 
					                   )
				                   ).ToList ();
				_filteredConferences = new ObservableCollection<Conference> (filteredList);
			} else {
				//_filteredConferences = this.ViewModel.Conferences;
			}

			TableView.ReloadData ();
		}
	}
    
}
