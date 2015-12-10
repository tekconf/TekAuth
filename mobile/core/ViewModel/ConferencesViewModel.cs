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
	public class ConferencesViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		public RelayCommand LoadConferencesCommand { get; private set; }
		public ConferencesViewModel (ISettingsService settingsService)
		{
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
			ITekConfApi api;
			if (!string.IsNullOrWhiteSpace (_settingsService.UserIdToken)) {
				api = RestService.For<ITekConfApi> (new HttpClient (new AuthenticatedHttpClientHandler (_settingsService.UserIdToken)) { 
					BaseAddress = new Uri ("https://tekauth.azurewebsites.net/api") 
				});
				var conferences = await api.GetConferences();
				this.Conferences = new ObservableCollection<Conference> (conferences);
				//AppDelegate.Conferences = conferences;
			} else {
				//api = RestService.For<ITekConfApi> ("https://tekauth.azurewebsites.net/api");

			}
		}

		public bool CanLoadConferences()
		{
			return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
		}
	}

}