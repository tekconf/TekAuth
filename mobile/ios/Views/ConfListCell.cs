using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;

namespace ios
{
	public partial class ConfListCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString ("ConfListCell");
		public static readonly UINib Nib;

		static ConfListCell ()
		{
			Nib = UINib.FromName ("ConfListCell", NSBundle.MainBundle);
		}

		public ConfListCell (IntPtr handle) : base (handle)
		{
		}
	}
}
