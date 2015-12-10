using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

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
	}
    
}