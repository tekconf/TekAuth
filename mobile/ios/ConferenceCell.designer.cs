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
		UILabel conferenceName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (conferenceName != null) {
				conferenceName.Dispose ();
				conferenceName = null;
			}
		}
	}
}
