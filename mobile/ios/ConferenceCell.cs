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
			conferenceContentView.Layer.BorderWidth = 1;
			//conferenceContentView.Layer.CornerRadius = 8;

		}
		public void SetConference(Conference conference)
		{
			this.conferenceName.Text = conference.Name;
		}

	}
}