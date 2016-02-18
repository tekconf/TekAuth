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

namespace ios.Views
{
	[Register ("ConferenceDetailView")]
	partial class ConferenceDetailView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel slug { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (slug != null) {
				slug.Dispose ();
				slug = null;
			}
		}
	}
}
