using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using Refit;
using System.Net.Http;
using System;
using System.Collections.ObjectModel;
using Tekconf.DTO;
using System.Collections.Generic;
using Akavache;
using Fusillade;
using System.Linq;

namespace TekConf.Mobile.Core.ViewModel
{
	public class MyConferencesViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		public RelayCommand LoadConferencesCommand { get; private set; }

		IConferencesService _conferencesService;

		public MyConferencesViewModel (ISettingsService settingsService, IConferencesService conferencesService)
		{
			this._conferencesService = conferencesService;
			_settingsService = settingsService;
			_myConferences = new ObservableCollection<Conference> ();
			this.LoadConferencesCommand = new RelayCommand(async () => await this.LoadMyConferences(Priority.UserInitiated), CanLoadMyConferences);
		}

		ObservableCollection<Conference> _myConferences;
		public ObservableCollection<Conference> MyConferences {
			get {
				return _myConferences;
			}
			private set {
				if (value == _myConferences) return;
				_myConferences = value;
				RaisePropertyChanged (() => MyConferences);
			}
		}

		public async Task LoadMyConferences(Priority priority)
		{
			//if (!string.IsNullOrWhiteSpace (_settingsService.UserIdToken)) {
			var conferences = await _conferencesService
				.GetConferences(_settingsService.UserIdToken, priority)
				.ConfigureAwait(false);

			//TODO : Better filter
			conferences = conferences.Take (2).ToList();
			this.MyConferences = new ObservableCollection<Conference> (conferences);

			//} else {
			//}
		}


		public bool CanLoadMyConferences()
		{
			return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
		}
	}

}