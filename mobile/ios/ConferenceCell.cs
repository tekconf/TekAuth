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

		public void SetConference(Conference conference)
		{
			this.conferenceName.Text = conference.Name;
		}

	}
}