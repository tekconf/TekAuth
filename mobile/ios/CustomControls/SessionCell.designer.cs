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
		UIView sessionRoomView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionSpeaker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView sessionSpeakerView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView sessionTimeView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
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
			if (sessionRoomView != null) {
				sessionRoomView.Dispose ();
				sessionRoomView = null;
			}
			if (sessionSpeaker != null) {
				sessionSpeaker.Dispose ();
				sessionSpeaker = null;
			}
			if (sessionSpeakerView != null) {
				sessionSpeakerView.Dispose ();
				sessionSpeakerView = null;
			}
			if (sessionTimeView != null) {
				sessionTimeView.Dispose ();
				sessionTimeView = null;
			}
			if (sessionTitle != null) {
				sessionTitle.Dispose ();
				sessionTitle = null;
			}
		}
	}
}
