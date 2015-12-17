// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ios
{
	[Register ("SessionCell")]
	partial class SessionCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView conferenceDateView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView conferenceFavoriteView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView conferenceLocationView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView highlightColorBar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView sessionContentView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionRoom { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionSpeaker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (conferenceDateView != null) {
				conferenceDateView.Dispose ();
				conferenceDateView = null;
			}
			if (conferenceFavoriteView != null) {
				conferenceFavoriteView.Dispose ();
				conferenceFavoriteView = null;
			}
			if (conferenceLocationView != null) {
				conferenceLocationView.Dispose ();
				conferenceLocationView = null;
			}
			if (highlightColorBar != null) {
				highlightColorBar.Dispose ();
				highlightColorBar = null;
			}
			if (sessionContentView != null) {
				sessionContentView.Dispose ();
				sessionContentView = null;
			}
			if (sessionDate != null) {
				sessionDate.Dispose ();
				sessionDate = null;
			}
			if (sessionDescription != null) {
				sessionDescription.Dispose ();
				sessionDescription = null;
			}
			if (sessionRoom != null) {
				sessionRoom.Dispose ();
				sessionRoom = null;
			}
			if (sessionSpeaker != null) {
				sessionSpeaker.Dispose ();
				sessionSpeaker = null;
			}
			if (sessionTitle != null) {
				sessionTitle.Dispose ();
				sessionTitle = null;
			}
		}
	}
}
