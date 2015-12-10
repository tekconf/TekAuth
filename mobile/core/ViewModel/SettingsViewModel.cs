using GalaSoft.MvvmLight;
using PropertyChanged;

namespace TekConf.Mobile.Core.ViewModel
{
	[ImplementPropertyChanged]
    public class SettingsViewModel : ViewModelBase
    {
		public string Nickname {get;set;}
		public string Email {get;set;}
//		string _nickname;
//
//		public string Nickname {
//			get {
//				return _nickname;
//			}
//			set {
//				if (value == _nickname) return;
//				_nickname = value;
//				RaisePropertyChanged (() => Nickname);
//			}
//		}
//
//		string _email;
//
//		public string Email {
//			get {
//				return _email;
//			}
//			set {
//				if (value == _email) return;
//				_email = value;
//				RaisePropertyChanged (() => Email);
//			}
//		}
    }
}