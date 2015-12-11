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
		

		public ConferenceDetailViewModel (Conference conference)
		{
			Conference = conference;

			Name = conference.Name;
			Description = conference.Description;
			StartDate = conference.StartDate;
			EndDate = conference.EndDate;

		}

		public Conference Conference { get; private set;}

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

		private string _description;
		public string Description {
			get {
				return _description;
			}
			set {
				if (value == _name) return;
				_description = value;
				RaisePropertyChanged (() => Description);
			}
		}

		private DateTime? _startDate;
		public DateTime? StartDate {
			get {
				return _startDate;
			}
			set {
				if (value == _startDate) return;
				_startDate = value;
				RaisePropertyChanged (() => StartDate);
			}
		}

		private DateTime? _endDate;
		public DateTime? EndDate {
			get {
				return _endDate;
			}
			set {
				if (value == _endDate) return;
				_endDate = value;
				RaisePropertyChanged (() => EndDate);
			}
		}

	}
}