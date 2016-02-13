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
	[Register ("ConferenceDetailViewController")]
	partial class ConferenceDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton addToMySchedule { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton conferenceAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView conferenceImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MapKit.MKMapView conferenceMap { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceStartDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton viewSessions { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (addToMySchedule != null) {
				addToMySchedule.Dispose ();
				addToMySchedule = null;
			}
			if (conferenceAddress != null) {
				conferenceAddress.Dispose ();
				conferenceAddress = null;
			}
			if (conferenceDescription != null) {
				conferenceDescription.Dispose ();
				conferenceDescription = null;
			}
			if (conferenceImage != null) {
				conferenceImage.Dispose ();
				conferenceImage = null;
			}
			if (conferenceMap != null) {
				conferenceMap.Dispose ();
				conferenceMap = null;
			}
			if (conferenceName != null) {
				conferenceName.Dispose ();
				conferenceName = null;
			}
			if (conferenceStartDate != null) {
				conferenceStartDate.Dispose ();
				conferenceStartDate = null;
			}
			if (viewSessions != null) {
				viewSessions.Dispose ();
				viewSessions = null;
			}
		}
	}
}
