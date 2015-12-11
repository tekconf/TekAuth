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
		UILabel conferenceDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel conferenceEndDate { get; set; }

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
		UIView highlightColor { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (conferenceDescription != null) {
				conferenceDescription.Dispose ();
				conferenceDescription = null;
			}
			if (conferenceEndDate != null) {
				conferenceEndDate.Dispose ();
				conferenceEndDate = null;
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
			if (highlightColor != null) {
				highlightColor.Dispose ();
				highlightColor = null;
			}
		}
	}
}
