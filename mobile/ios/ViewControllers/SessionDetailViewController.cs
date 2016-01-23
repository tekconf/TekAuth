using System;
using UIKit;
using TekConf.Mobile.Core.ViewModels;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core.Services;
using Foundation;
using System.Linq;

namespace ios
{
	partial class SessionDetailViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		public SessionDetailViewController (IntPtr handle) : base (handle)
		{
		}

		private SessionDetailViewModel Vm {
			get {
				return Application.Locator.Session;
			}
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var speaker = this.Vm.Session.Speakers.ToArray()[indexPath.Row];
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "sessionSpeakerCell");

			var font = UIFont.FromName("OpenSans-Light", 14f);
			if (font != null)
			{
				cell.TextLabel.Font = font;
			}
			cell.TextLabel.Text = string.Format("{0} {1}", speaker.FirstName, speaker.LastName);

			return cell;
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			if (this.Vm == null 
			    || this.Vm.Session == null 
			    || this.Vm.Session.Speakers == null)
			{
				return 0;
			}

			return this.Vm.Session.Speakers.Count();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			speakersList.WeakDataSource = this;

			addToMySchedule.Layer.BorderColor = UIColor.LightGray.CGColor;
			addToMySchedule.Layer.BorderWidth = 0.5f;

			addToMySchedule.TouchUpInside += (sender, e) => {
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