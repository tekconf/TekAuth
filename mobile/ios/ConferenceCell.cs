using System;
using UIKit;
using Tekconf.DTO;
using CoreGraphics;

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
			highlightColorBar.BackgroundColor = UIColor.FromRGB (red: 0, green: 0, blue: 0);

			if (conference.StartDate.HasValue) {
				this.conferenceDate.Text = conference.StartDate.Value.ToShortDateString ();
			}
			this.conferenceDescription.Text = conference.Description;

			this.conferenceLocation.Text = "San Francisco, CA";
		}

	}

	public static class UIColorExtensions
	{
		public static UIColor FromHex(this UIColor color,int hexValue)
		{
			return UIColor.FromRGB(
				(((float)((hexValue & 0xFF0000) >> 16))/255.0f),
				(((float)((hexValue & 0xFF00) >> 8))/255.0f),
				(((float)(hexValue & 0xFF))/255.0f)
			);
		}
	}
}