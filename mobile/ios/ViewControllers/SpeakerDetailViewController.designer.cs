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
	[Register ("SpeakerDetailViewController")]
	partial class SpeakerDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView speakerImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel speakerName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (speakerImage != null) {
				speakerImage.Dispose ();
				speakerImage = null;
			}
			if (speakerName != null) {
				speakerName.Dispose ();
				speakerName = null;
			}
		}
	}
}
