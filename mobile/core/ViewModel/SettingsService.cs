
namespace TekConf.Mobile.Core.ViewModel
{
	public class SettingsService : ISettingsService
	{
		private string _userIdToken;
		public string UserIdToken {
			get {
				return _userIdToken;
			}
			set {
				_userIdToken = value;
			}
		}		

		private string _auth0Domain;

		public string Auth0Domain { 
			get {
				return "tekconf.auth0.com";
			}   
		}

		public string Auth0ClientId {
			get {
				return "XhxV5TtBdzUwth21O4jhvITp5I9hJ6xS";
			}
		}
	}
    
}