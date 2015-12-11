using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using Refit;
using System.Net.Http;
using System;
using System.Collections.ObjectModel;
using Tekconf.DTO;

namespace TekConf.Mobile.Core.ViewModel
{
	public class ConferenceDetailViewModel : ViewModelBase
	{
		Conference _conference;

		public ConferenceDetailViewModel (Conference conference)
		{
			_conference = conference;
			Name = conference.Name;
		}

		private string _name;

		public string Name {
			get {
				return _name;
			}
			set {
				if (value == _name) return;
				_name = value;
				RaisePropertyChanged (() => Name);
			}
		}
	}

}