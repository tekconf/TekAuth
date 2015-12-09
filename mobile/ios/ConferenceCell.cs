using System;
using UIKit;
using Tekconf.DTO;
using CoreGraphics;
using System.Globalization;

namespace ios
{
	partial class ConferenceCell : UITableViewCell
	{
		public ConferenceCell (IntPtr handle) : base (handle)
		{
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			conferenceContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			conferenceContentView.Layer.BorderWidth = 0.5f;
		}

		public void SetConference (Conference conference)
		{
			this.conferenceName.Text = conference.Name;
			highlightColorBar.BackgroundColor = UIColorExtensions.FromHex (conference.HighlightColor);

			if (conference.StartDate.HasValue) {
				this.conferenceDate.Text = conference.StartDate.Value.ToShortDateString ();
			}
			this.conferenceDescription.Text = conference.Description;

			this.conferenceLocation.Text = "San Francisco, CA";
		}

	}

	public static class UIColorExtensions
	{
		public static UIColor FromHex(string hexColor)
		{
			if (!string.IsNullOrWhiteSpace (hexColor)) {
				var red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
				var green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
				var blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);

				return UIColor.FromRGB (red, green, blue);

			} else {

				return UIColor.Green;
			}


//			return UIColor.FromRGB(
//				(((float)((hexValue & 0xFF0000) >> 16))/255.0f),
//				(((float)((hexValue & 0xFF00) >> 8))/255.0f),
//				(((float)(hexValue & 0xFF))/255.0f)
//			);
		}
	}
}