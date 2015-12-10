using System;
using UIKit;
using Auth0.SDK;
using Refit;
using TekConf.Mobile.Core;
using System.Net.Http;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Helpers;
using TekConf.Mobile.Core.ViewModel;

namespace ios
{
	partial class SettingsViewController : UIViewController
	{
		private Auth0Client _auth0;
		private Binding<string, string> _nicknameLabelBinding;
		private Binding<string, string> _emailLabelBinding;

		public SettingsViewController (IntPtr handle) : base (handle)
		{
			_auth0 = new Auth0Client (
				"tekconf.auth0.com",
				"XhxV5TtBdzUwth21O4jhvITp5I9hJ6xS"
			);
		}
		private SettingsViewModel Vm {
			get {
				return Application.Locator.Settings;
			}
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_nicknameLabelBinding = this.SetBinding (
				() => Vm.Nickname,
				() => nickname.Text);

			_emailLabelBinding = this.SetBinding (
				() => Vm.Email,
				() => email.Text);

			loginButton.TouchUpInside += async (sender, e) => {
				Auth0User user = null;
				try {
					user = await _auth0.LoginAsync (this, scope: "openid profile");
				} catch (OperationCanceledException) {
					
				}

				if (user != null) {
					//nickname.Text = user.Profile.GetValue ("nickname").ToString ();
					Vm.Nickname = user.Profile.GetValue ("nickname").ToString ();
					Vm.Email = user.Profile.GetValue ("email").ToString ();

					await AppDelegate.LoadConferences (user.IdToken);
				}
			};
		}
	}
}
