using System;
using UIKit;
using Tekconf.DTO;

namespace ios
{
	partial class SessionCell : UITableViewCell
	{
		public SessionCell (IntPtr handle) : base (handle)
		{
		}

		public void SetSession(Session session)
		{
			sessionTitle.Text = session.Title;
			sessionTime.Text = session.StartDate.Value.ToShortTimeString ();
			sessionRoom.Text = session.Room;
			sessionSpeaker.Text = session.SpeakerName;
		}
	}
}
