using System;
using UIKit;
using Auth0.SDK;
using Refit;
using TekConf.Mobile.Core;
using System.Net.Http;
using System.Linq;
using System.Text;

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

			var fontList = new StringBuilder ();
			var familyNames = UIFont.FamilyNames;

			loginButton.TouchUpInside += async (sender, e) => {
				Auth0User user = null;
				try {
					user = await _auth0.LoginAsync (this, scope: "openid profile");
				} catch (OperationCanceledException) {
					
				}

				if (user != null) {
					nickname.Text = user.Profile.GetValue ("nickname").ToString ();
					email.Text = user.Profile.GetValue ("email").ToString ();

					await AppDelegate.LoadConferences (user.IdToken);
				}
			};
		}
	}
}
