using System;
using UIKit;

namespace ios
{
	partial class UnauthenticatedView : UIView
	{
		public UnauthenticatedView (IntPtr handle) : base (handle)
		{

		}

		public override void Draw (CoreGraphics.CGRect rect)
		{
			base.Draw (rect);

			logo.Font = UIFont.FromName("FontAwesome", 168f);
			logo.Text = "\xf110";
			logo.SizeToFit ();

			this.SetNeedsUpdateConstraints ();
		}
	}
}