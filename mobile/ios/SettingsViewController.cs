using System;
using UIKit;
using Auth0.SDK;
using Refit;
using TekConf.Mobile.Core;
using System.Net.Http;
using System.Linq;

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

					var api = RestService.For<ITekConfApi>(new HttpClient(new AuthenticatedHttpClientHandler(user.IdToken)) { 
						BaseAddress = new Uri("https://tekauth.azurewebsites.net/api") 
					});

					var conferences = await api.GetConferences();
					AppDelegate.Conferences = conferences;
				}
			};
		}
	}
}
