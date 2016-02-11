using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ios
{
	partial class ConferencesFilterViewController : UIViewController
	{
		public ConferencesFilterViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			saveFilter.TouchUpInside += (sender, e) => {
				this.DismissModalViewController(animated: true);
			};

			resetFilter.TouchUpInside += (sender, e) => {
				this.DismissModalViewController(animated: true);
			};
		}
	}
}
