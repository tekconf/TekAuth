using System;
using UIKit;
using Auth0.SDK;

namespace ios
{
	partial class SettingsViewController : UIViewController
	{
		private Auth0Client _auth0;

		public SettingsViewController (IntPtr handle) : base (handle)
		{
			_auth0 = new Auth0Client (
				"tekconf.auth0.com",
				"XhxV5TtBdzUwth21O4jhvITp5I9hJ6xS"
			);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			loginButton.TouchUpInside += async (sender, e) => {
				var user = await _auth0.LoginAsync(this, scope: "openid profile");
				if (user != null) {
					nickname.Text = user.Profile.GetValue("nickname").ToString();
					email.Text = user.Profile.GetValue("email").ToString();
				}
			};
		}
	}
}
