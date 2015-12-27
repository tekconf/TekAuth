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

			//this.Title = Vm.ConferenceName;
			sessionTitle.Text = Vm.Title;
			sessionDescription.Text = Vm.Description;
			sessionRoom.Text = Vm.Room;
			sessionTime.Text = Vm.DateRange;
		}
	}
}
