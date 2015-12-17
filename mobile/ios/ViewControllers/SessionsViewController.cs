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
				Description = "Have you been telling yourself you're going to do mobile, but never got around to it? Wouldn't it be awesome if you could leverage your existing web skills to quickly build native mobile apps? In this workshop we'll do exactly that using Cordova. Cordova is a platform for building native mobile applications using HTML, CSS and JavaScript. By the time you leave this workshop, you'll have built an app that can be deployed to iOs, Android, and just about any other mobile device.",
				Id = 1,
				Room = "Ballroom A",
				SpeakerName = "Rob Gibbens",
				StartDate = new DateTime(2016, 04, 16, 13, 0,0),
				EndDate = new DateTime(2016, 04, 16, 14, 0,0),

			}, 
			new Session { 
				Title = "Acceptance Test Driven Development by example with Cucumber – Part 1",
				Description = "Are you a developer or tester who wants to help prevent bugs instead of finding them? Have you heard of Cucumber and wonder what it was? Then this session is for you! Automated testing is taking over the software industry, however writing tests after development is done only is the start of it. When developers, qa, and business owners kick start features by creating and automating test cases together, the team gets the added benefit of building the right functionality. This hands on workshop will have attendees working through examples using ruby gems, such as Cucumber, Watir, and PageObject, and will cover core concepts such as creating automatable acceptance criteria, how to keep ...",
				Id = 1,
				Room = "Ballroom A",
				SpeakerName = "Rob Gibbens",
				StartDate = new DateTime(2016, 04, 16, 13, 0,0),
				EndDate = new DateTime(2016, 04, 16, 14, 0,0),
			}, 
			new Session { 
				Title = "Cross Platform Mobile UI with Xamarin Forms Workshop",
				Description = "Xamarin Forms is a powerful cross-platform mobile UI toolkit built on top of Xamarin's cross-platform mobile framework. In this workshop we'll start with a blank solution and learn how to use Xamarin Forms, step-by-step, to build a working mobile app before the day is done. Along the way we'll discuss the pros-and-cons of Forms vs Xamarin \"Classic\" vs Native development, how to maximize code sharing across platforms, and make plenty of stops to answer your questions. Topics covered include: * XF UI concepts * XAML support * MVVM and data binding * Navigation * Accessing the native platform via Dependency Injection * Advanced UI with Custom Renderers * Webservices * UI Styling ...",
				Id = 1,
				Room = "Ballroom A",
				SpeakerName = "Jason Awbrey",
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

//		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			return 221;
//		}

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
