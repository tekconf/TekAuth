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
		UIView sessionContentView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionRoom { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionSpeaker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (sessionContentView != null) {
				sessionContentView.Dispose ();
				sessionContentView = null;
			}
			if (sessionRoom != null) {
				sessionRoom.Dispose ();
				sessionRoom = null;
			}
			if (sessionSpeaker != null) {
				sessionSpeaker.Dispose ();
				sessionSpeaker = null;
			}
			if (sessionTime != null) {
				sessionTime.Dispose ();
				sessionTime = null;
			}
			if (sessionTitle != null) {
				sessionTitle.Dispose ();
				sessionTitle = null;
			}
		}
	}
}
