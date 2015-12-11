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
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ios
{
	partial class SettingsViewController : UIViewController
	{
		private Auth0Client _auth0;
		private Binding<string, string> _nicknameLabelBinding;
		private Binding<string, string> _emailLabelBinding;
		private readonly ISettingsService _settingsService;

		public SettingsViewController (IntPtr handle) : base (handle)
		{
			_settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();

			_auth0 = new Auth0Client (
				_settingsService.Auth0Domain,
				_settingsService.Auth0ClientId
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
					Vm.Nickname = user.Profile.GetValue ("nickname").ToString ();
					Vm.Email = user.Profile.GetValue ("email").ToString ();
					_settingsService.UserIdToken = user.IdToken;
					//await Vm.LoadConferences();
					//await AppDelegate.LoadConferences (user.IdToken);
				}
			};
		}
	}
}
