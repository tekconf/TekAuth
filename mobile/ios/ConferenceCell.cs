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
		public void SetConference(Conference conference)
		{
			this.conferenceName.Text = conference.Name;
			if (conference.StartDate.HasValue) {
				this.conferenceDate.Text = conference.StartDate.Value.ToShortDateString ();
			}
			this.conferenceDescription.Text = conference.Description;

			this.conferenceLocation.Text = "San Francisco, CA";
		}

	}
}