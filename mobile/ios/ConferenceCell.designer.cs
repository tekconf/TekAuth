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
	[Register ("ConferenceCell")]
	partial class ConferenceCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView conferenceContentView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView conferenceDateView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView conferenceLocationView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView highlightColorBar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (conferenceContentView != null) {
				conferenceContentView.Dispose ();
				conferenceContentView = null;
			}
			if (conferenceDate != null) {
				conferenceDate.Dispose ();
				conferenceDate = null;
			}
			if (conferenceDateView != null) {
				conferenceDateView.Dispose ();
				conferenceDateView = null;
			}
			if (conferenceDescription != null) {
				conferenceDescription.Dispose ();
				conferenceDescription = null;
			}
			if (conferenceLocation != null) {
				conferenceLocation.Dispose ();
				conferenceLocation = null;
			}
			if (conferenceLocationView != null) {
				conferenceLocationView.Dispose ();
				conferenceLocationView = null;
			}
			if (conferenceName != null) {
				conferenceName.Dispose ();
				conferenceName = null;
			}
			if (highlightColorBar != null) {
				highlightColorBar.Dispose ();
				highlightColorBar = null;
			}
		}
	}
}
