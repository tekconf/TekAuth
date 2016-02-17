using Foundation;
using UIKit;
using CoreSpotlight;
using TekConf.Mobile.Core.ViewModels;
using Fusillade;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core.Services;
using WindowsAzure.Messaging;
using GalaSoft.MvvmLight.Messaging;
using TekConf.Mobile.Core.Messages;

namespace ios
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

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

			var pushSettings = UIUserNotificationSettings.GetSettingsForTypes (
				                   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
				                   new NSSet ());

			UIApplication.SharedApplication.RegisterUserNotificationSettings (pushSettings);
			UIApplication.SharedApplication.RegisterForRemoteNotifications ();

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
			var settingsService = ServiceLocator.Current.GetInstance<ISettingsService> ();
			var schedulesService = ServiceLocator.Current.GetInstance<ISchedulesService> ();

			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				//var tabController = this.Window.RootViewController as UITabBarController;
				var navController = this.Window.RootViewController as UINavigationController;

				var identifier = userActivity?.UserInfo?.ObjectForKey (CSSearchableItem.ActivityIdentifier);

				if (identifier != null && identifier.ToString ().Contains ("|\\/|")) {
					// This is a session or a speaker
					var parts = identifier.ToString ().Split (new [] { "|\\/|" }, StringSplitOptions.RemoveEmptyEntries);
					if (parts.Length == 2) {
						//This is a session
						var conferenceSlug = parts [0];
						var sessionSlug = parts [1];

						var conferencesViewModel = Application.Locator.Conferences;
					    conferencesViewModel.LoadConferencesCommand.Execute(Priority.Explicit);

						var conference = conferencesViewModel.Conferences.Single (c => c.Slug == conferenceSlug);
						var session = conference.Sessions.Single (s => s.Slug == sessionSlug);
						var conferenceVm = new ConferenceDetailViewModel (conference, schedulesService, settingsService);
						var sessionVm = new SessionDetailViewModel (session, conference.Name);

						Application.Locator.Conference = conferenceVm;
						Application.Locator.Session = sessionVm;

						var storyboard = UIStoryboard.FromName ("Main", null);

						var conferencesViewController = storyboard.InstantiateViewController ("ConferencesViewController") as ConferencesViewController;

						var conferenceDetailViewController = storyboard.InstantiateViewController ("ConferenceDetailViewController") as ConferenceDetailViewController;
						var sessionsViewController = storyboard.InstantiateViewController ("SessionsViewController") as SessionsViewController;
						var sessionDetailViewController = storyboard.InstantiateViewController ("SessionDetailViewController");

						navController.SetViewControllers (new UIViewController[] {
							conferencesViewController,
							conferenceDetailViewController,
							sessionsViewController,
							sessionDetailViewController
						}, animated: false);
					} else if (parts.Length == 3) {
						//This is a speaker
						var conferenceSlug = parts [0];
						var sessionSlug = parts [1];
						var speakerSlug = parts [2];

						var conferencesViewModel = Application.Locator.Conferences;
                        conferencesViewModel.LoadConferencesCommand.Execute(Priority.Explicit);

                        //var task = Task.Run (async () => { 
                        //	await conferencesViewModel.LoadConferences (Priority.Explicit); 
                        //});
                        //task.Wait ();
                        var conference = conferencesViewModel.Conferences.Single (c => c.Slug == conferenceSlug);
						var session = conference.Sessions.Single (s => s.Slug == sessionSlug);
						var speaker = session.Speakers.Single (s => s.Slug == speakerSlug);
						var conferenceVm = new ConferenceDetailViewModel (conference, schedulesService, settingsService);
						var sessionVm = new SessionDetailViewModel (session, conference.Name);
						var speakerVm = new SpeakerDetailViewModel (session, speaker);

						Application.Locator.Conference = conferenceVm;
						Application.Locator.Session = sessionVm;
						Application.Locator.Speaker = speakerVm;

						var storyboard = UIStoryboard.FromName ("Main", null);

						var conferencesViewController = storyboard.InstantiateViewController ("ConferencesViewController") as ConferencesViewController;

						var conferenceDetailViewController = storyboard.InstantiateViewController ("ConferenceDetailViewController") as ConferenceDetailViewController;
						var sessionsViewController = storyboard.InstantiateViewController ("SessionsViewController") as SessionsViewController;
						var sessionDetailViewController = storyboard.InstantiateViewController ("SessionDetailViewController");
						var speakerDetailViewController = storyboard.InstantiateViewController ("SpeakerDetailViewController");

						navController.SetViewControllers (new UIViewController[] {
							conferencesViewController,
							conferenceDetailViewController,
							sessionsViewController,
							sessionDetailViewController,
							speakerDetailViewController
						}, animated: false);
					}
				} else {
					// This is a conference
					var conferenceSlug = identifier.ToString ();

					var conferencesViewModel = Application.Locator.Conferences;
                    conferencesViewModel.LoadConferencesCommand.Execute(Priority.Explicit);

                    //var task = Task.Run (async () => {
                    //	await conferencesViewModel.LoadConferences (Priority.Explicit);
                    //});
                    //task.Wait ();
                    var conference = conferencesViewModel.Conferences.Single (c => c.Slug == conferenceSlug);
					var vm = new ConferenceDetailViewModel (conference, schedulesService, settingsService);
					Application.Locator.Conference = vm;

					var storyboard = UIStoryboard.FromName ("Main", null);
					var conferenceDetailViewController = storyboard.InstantiateViewController ("ConferenceDetailViewController") as ConferenceDetailViewController;
					;

					navController.SetViewControllers (new UIViewController[] { conferenceDetailViewController }, animated: false);
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
			
			//UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(red: 34, green: 91, blue: 149);
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB (red: 128, green: 153, blue: 77);
			UIBarButtonItem.Appearance.TintColor = UIColor.White;

			UINavigationBar.Appearance.TintColor = UIColor.White;
			var navStyle = new UITextAttributes () {
				TextColor = UIColor.White,
				TextShadowColor = UIColor.Clear,

				Font = UIFont.FromName ("OpenSans-Light", 16f)
			};

			UINavigationBar.Appearance.SetTitleTextAttributes (navStyle); 
			UIImageView.AppearanceWhenContainedIn (typeof(UINavigationBar)).TintColor = UIColor.White;
			UIBarButtonItem.Appearance.SetTitleTextAttributes (navStyle, UIControlState.Normal);
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{

			if (deviceToken != null) {
				Hub = new SBNotificationHub (Constants.ConnectionString, Constants.NotificationHubPath);

				Hub.UnregisterAllAsync (deviceToken, (error) => {
					if (error != null) {
						Console.WriteLine ("Error calling Unregister: {0}", error.ToString ());
						return;
					}
					NSSet tags = null;
					var settingsService = ServiceLocator.Current.GetInstance<ISettingsService> ();
					if (!string.IsNullOrWhiteSpace (settingsService.EmailAddress)) {
						tags = new NSSet (new string[] {
							"platform:iOS",
							"emailAddress:" + settingsService.EmailAddress
						});
					} else {
						tags = new NSSet (new string[] {
							"platform:iOS"
						});
					}

					Hub.RegisterNativeAsync (deviceToken, tags, (errorCallback) => {
						if (errorCallback != null)
							Console.WriteLine ("RegisterNativeAsync error: " + errorCallback.ToString ());
					});


				});
			}
		}

		private static SBNotificationHub Hub { get; set; }

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			ProcessNotification (userInfo, false);
		}

		void ProcessNotification (NSDictionary options, bool fromFinishedLaunching)
		{
			// Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
			if (null != options && options.ContainsKey (new NSString ("aps"))) {
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey (new NSString ("aps")) as NSDictionary;

				string command = string.Empty;

				//Extract the alert text
				// NOTE: If you're using the simple alert by just specifying
				// "  aps:{alert:"alert msg here"}  ", this will work fine.
				// But if you're using a complex alert with Localization keys, etc.,
				// your "alert" object from the aps dictionary will be another NSDictionary.
				// Basically the JSON gets dumped right into a NSDictionary,
				// so keep that in mind.
				if (aps.ContainsKey (new NSString ("command"))) {
					command = (aps [new NSString ("command")] as NSString).ToString ();
				}
				//If this came from the ReceivedRemoteNotification while the app was running,
				// we of course need to manually process things like the sound, badge, and alert.
				if (!fromFinishedLaunching) {
					//Manually show an alert
					if (!string.IsNullOrEmpty (command)) {
						if (command == RemoteCommands.Alert) {
							UIAlertView avAlert = new UIAlertView ("Notification", "Alert", null, "OK", null);
							avAlert.Show ();
						} else if (command == RemoteCommands.RefreshConferences) {
							Messenger.Default.Send (new ConferenceAddedMessage ());
						} else if (command == RemoteCommands.RefreshSchedule) {
							Messenger.Default.Send (new ConferenceAddedToScheduleMessage ());
						} 
					}
				}
			}
		}
	}
}