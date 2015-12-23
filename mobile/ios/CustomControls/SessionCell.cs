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

		Session _session;

		public void SetSession(Session session, string highlightColor)
		{
			_session = session;

			sessionContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			sessionContentView.Layer.BorderWidth = 0.5f;

			highlightColorBar.BackgroundColor = UIColorExtensions.FromHex (highlightColor);
			sessionTitle.Text = _session.Title;
			sessionDate.Text = _session.StartDate.Value.ToShortTimeString ();
			sessionRoom.Text = _session.Room;
			sessionSpeaker.Text = _session.SpeakerName;
			sessionDescription.Text = _session.Description;
			sessionSpeakerView.BackgroundColor = UIColorExtensions.FromHex (highlightColor);

			sessionTitle.SizeToFit ();
			sessionDescription.SizeToFit ();
		}

	}
}
