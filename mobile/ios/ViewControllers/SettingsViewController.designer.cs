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
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton closeSettings { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel email { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView loggedInView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton loginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel nickname { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (closeSettings != null) {
				closeSettings.Dispose ();
				closeSettings = null;
			}
			if (email != null) {
				email.Dispose ();
				email = null;
			}
			if (loggedInView != null) {
				loggedInView.Dispose ();
				loggedInView = null;
			}
			if (loginButton != null) {
				loginButton.Dispose ();
				loginButton = null;
			}
			if (nickname != null) {
				nickname.Dispose ();
				nickname = null;
			}
		}
	}
}
