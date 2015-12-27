using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Tekconf.DTO;
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

			sessionTitle.Text = Vm.Title;
			sessionDescription.Text = Vm.Description;
			sessionTime.Text = Vm.DateRange;
		}
	}
}
