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
	[Register ("SessionDetailViewController")]
	partial class SessionDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton addToMySchedule { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sessionTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (addToMySchedule != null) {
				addToMySchedule.Dispose ();
				addToMySchedule = null;
			}
			if (sessionDescription != null) {
				sessionDescription.Dispose ();
				sessionDescription = null;
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
