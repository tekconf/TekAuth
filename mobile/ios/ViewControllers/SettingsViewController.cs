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
using Xamarin;

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
				} catch (OperationCanceledException opEx) {
					Insights.Report (opEx);
				}

				if (user != null) {
					var emailAddress = user.Profile.GetValue ("email").ToString ();
					var nicknameValue = user.Profile.GetValue ("nickname").ToString ();
					Insights.Identify(emailAddress, null);
					Insights.Identify(null, Insights.Traits.Email, emailAddress);
					Insights.Identify(null, Insights.Traits.Name, nicknameValue);

					Vm.Nickname = nicknameValue;
					Vm.Email = emailAddress;
					_settingsService.UserIdToken = user.IdToken;

				}
			};
		}
	}
}
