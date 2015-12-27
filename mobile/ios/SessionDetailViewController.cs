using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ios
{
	partial class SessionDetailViewController : UIViewController
	{
		public SessionDetailViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			sessionTitle.Text = "Title";
		}
	}
}
