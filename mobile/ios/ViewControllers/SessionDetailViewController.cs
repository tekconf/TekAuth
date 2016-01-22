using System;
using UIKit;
using TekConf.Mobile.Core.ViewModels;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core.Services;

namespace ios
{
	partial class SessionDetailViewController : UIViewController
	{
		public SessionDetailViewController (IntPtr handle) : base (handle)
		{
		}

		private SessionDetailViewModel Vm {
			get {
				return Application.Locator.Session;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			addToMySchedule.Layer.BorderColor = UIColor.LightGray.CGColor;
			addToMySchedule.Layer.BorderWidth = 0.5f;

			addToMySchedule.TouchUpInside += async (sender, e) => {
				var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
				if (!string.IsNullOrWhiteSpace (settingsService.UserIdToken)) {
				} else {
					new UIAlertView("Login", "You must login to add a session to your schedule", null, "Ok", null).Show();
				}
			};

			sessionTitle.Text = Vm.Title ?? string.Empty;
			sessionDescription.Text = Vm.Description ?? string.Empty;
			sessionRoom.Text = Vm.Room ?? string.Empty;
			sessionTime.Text = Vm.DateRange ?? string.Empty;
		}
	}
}