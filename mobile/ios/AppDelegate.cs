using Foundation;
using UIKit;
using CoreSpotlight;
using TekConf.Mobile.Core.ViewModel;
using Fusillade;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core;

namespace ios
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}

//		public static async Task LoadConferences(string token)
//		{
//			ITekConfApi api;
//			if (!string.IsNullOrWhiteSpace (token)) {
//				api = RestService.For<ITekConfApi> (new HttpClient (new AuthenticatedHttpClientHandler (token)) { 
//					BaseAddress = new Uri ("https://tekauth.azurewebsites.net/api") 
//				});
//				var conferences = await api.GetConferences();
//				AppDelegate.Conferences = conferences;
//			} else {
//				//api = RestService.For<ITekConfApi> ("https://tekauth.azurewebsites.net/api");
//			
//			}
//
//
//
//		}
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			#if DEBUG
			//Xamarin.Calabash.Start();
			#endif

			AdjustDefaultUI ();

			return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
			var schedulesService = ServiceLocator.Current.GetInstance<ISchedulesService>();

			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				var tabController = this.Window.RootViewController as UITabBarController;
				var navController = tabController.ViewControllers[0] as UINavigationController;

				var identifier = userActivity?.UserInfo?.ObjectForKey (CSSearchableItem.ActivityIdentifier);

				if (identifier != null && identifier.ToString ().Contains ("|\\/|")) {
					// This is a session
					var parts = identifier.ToString().Split(new [] {"|\\/|"}, System.StringSplitOptions.RemoveEmptyEntries);
					var conferenceSlug = parts[0];
					var sessionSlug = parts[1];

					var conferencesViewModel = Application.Locator.Conferences;
					var task = Task.Run(async () => { 
						await conferencesViewModel.LoadConferences (Priority.Explicit); 
					});
					task.Wait();
					var conference = conferencesViewModel.Conferences.Single(c => c.Slug == conferenceSlug);
					var session = conference.Sessions.Single (s => s.Slug == sessionSlug);
					var conferenceVm = new ConferenceDetailViewModel(conference, schedulesService, settingsService);
					var sessionVm = new SessionDetailViewModel (session, conference.Name);

					Application.Locator.Conference = conferenceVm;
					Application.Locator.Session = sessionVm;

					var storyboard = UIStoryboard.FromName ("Main", null);

					var conferencesViewController = storyboard.InstantiateViewController ("ConferencesViewController") as ConferencesViewController;

					var conferenceDetailViewController = storyboard.InstantiateViewController ("ConferenceDetailViewController") as ConferenceDetailViewController;
					var sessionsViewController = storyboard.InstantiateViewController ("SessionsViewController") as SessionsViewController;
					var sessionDetailViewController = storyboard.InstantiateViewController ("SessionDetailViewController");

					navController.SetViewControllers (new UIViewController[] { conferencesViewController, conferenceDetailViewController, sessionsViewController, sessionDetailViewController }, animated:false);
				} else {
					// This is a conference
					var conferenceSlug = identifier.ToString();

					var conferencesViewModel = Application.Locator.Conferences;
					var task = Task.Run(async () => { await conferencesViewModel.LoadConferences (Priority.Explicit); });
					task.Wait();
					var conference = conferencesViewModel.Conferences.Single(c => c.Slug == conferenceSlug);
					var vm = new ConferenceDetailViewModel(conference, schedulesService, settingsService);
					Application.Locator.Conference = vm;

					var storyboard = UIStoryboard.FromName ("Main", null);
					var conferenceDetailViewController = storyboard.InstantiateViewController ("ConferenceDetailViewController") as ConferenceDetailViewController;;

					navController.SetViewControllers (new UIViewController[] { conferenceDetailViewController }, animated:false);
				}
			}

			return true;
		}
		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

		private void AdjustDefaultUI ()
		{
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(red: 34, green: 91, blue: 149);
			UIBarButtonItem.Appearance.TintColor = UIColor.White;

			UINavigationBar.Appearance.TintColor = UIColor.White;
			var navStyle = new UITextAttributes () {
				TextColor = UIColor.White,
				TextShadowColor = UIColor.Clear,

				Font = UIFont.FromName ("Open Sans Light", 20f)
			};

			UINavigationBar.Appearance.SetTitleTextAttributes(navStyle); 
			UIImageView.AppearanceWhenContainedIn (typeof(UINavigationBar)).TintColor = UIColor.White;
			UIBarButtonItem.Appearance.SetTitleTextAttributes (navStyle, UIControlState.Normal);
		}
	}
}