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

namespace TekConf.Mobile.Core.ViewModel
{
	public class ConferencesViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		public RelayCommand LoadConferencesCommand { get; private set; }

		IConferencesService _conferencesService;

		public ConferencesViewModel (ISettingsService settingsService, IConferencesService conferencesService)
		{
			this._conferencesService = conferencesService;
			_settingsService = settingsService;
			_conferences = new ObservableCollection<Conference> ();
			this.LoadConferencesCommand = new RelayCommand(async () => await this.LoadConferences(), CanLoadConferences);
		}

		ObservableCollection<Conference> _conferences;
		public ObservableCollection<Conference> Conferences {
			get {
				return _conferences;
			}
			private set {
				if (value == _conferences) return;
				_conferences = value;
				RaisePropertyChanged (() => Conferences);
			}
		}

		public async Task LoadConferences()
		{
			//if (!string.IsNullOrWhiteSpace (_settingsService.UserIdToken)) {
				var conferences = await _conferencesService
					.GetConferences(_settingsService.UserIdToken, Priority.Explicit)
					.ConfigureAwait(false);
	
				this.Conferences = new ObservableCollection<Conference> (conferences);

			//} else {
			//}
		}


		public bool CanLoadConferences()
		{
			return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
		}
	}

}