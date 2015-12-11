using GalaSoft.MvvmLight;

namespace TekConf.Mobile.Core.ViewModel
{
	public class SettingsViewModel : ViewModelBase
    {

		private string _nickname;
		public string Nickname {
			get {
				return _nickname;
			}
			set {
				if (value == _nickname) return;
				_nickname = value;
				RaisePropertyChanged (() => Nickname);
			}
		}

		string _email;

		public string Email {
			get {
				return _email;
			}
			set {
				if (value == _email) return;
				_email = value;
				RaisePropertyChanged (() => Email);
			}
		}


    }


}