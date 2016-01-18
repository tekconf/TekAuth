using System;
using UIKit;
using Tekconf.DTO;

namespace ios
{
	partial class SessionCell : UITableViewCell
	{
		Session _session;

		public SessionCell (IntPtr handle) : base (handle)
		{
		}

		public void SetSession(Session session, string highlightColor)
		{
			_session = session;

			sessionContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			sessionContentView.Layer.BorderWidth = 0.5f;

			highlightColorBar.BackgroundColor = UIColorExtensions.FromHex (highlightColor);
			sessionSpeakerView.BackgroundColor = UIColorExtensions.FromHex (highlightColor);

			sessionTitle.Text = _session.Title ?? string.Empty;
			sessionDate.Text = _session.StartDate.HasValue ? 
				_session.StartDate.Value.ToShortTimeString () : string.Empty;

			sessionRoom.Text = _session.Room ?? string.Empty;
			sessionSpeaker.Text = _session.SpeakerName ();
			sessionDescription.Text = _session.Description ?? string.Empty;
		}
	}
}
