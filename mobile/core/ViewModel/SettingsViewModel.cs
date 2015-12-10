using GalaSoft.MvvmLight;

namespace TekConf.Mobile.Core.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SettingsViewModel : ViewModelBase
    {

		string _nickname;

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