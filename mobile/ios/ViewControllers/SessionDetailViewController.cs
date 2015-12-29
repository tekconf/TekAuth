using System;
using UIKit;
using TekConf.Mobile.Core.ViewModel;

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

			sessionTitle.Text = Vm.Title ?? string.Empty;
			sessionDescription.Text = Vm.Description ?? string.Empty;
			sessionRoom.Text = Vm.Room ?? string.Empty;
			sessionTime.Text = Vm.DateRange ?? string.Empty;
		}
	}
}