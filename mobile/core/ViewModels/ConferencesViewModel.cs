using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Tekconf.DTO;
using Fusillade;
using TekConf.Mobile.Core.Services;

namespace TekConf.Mobile.Core.ViewModels
{
	public class ConferencesViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		public RelayCommand LoadConferencesCommand { get; private set; }

	    private readonly IConferencesService _conferencesService;

		public ConferencesViewModel (ISettingsService settingsService, IConferencesService conferencesService)
		{
			_conferencesService = conferencesService;
			_settingsService = settingsService;
			_conferences = new ObservableCollection<Conference> ();

            this.LoadConferencesCommand = new RelayCommand(async () => await this.LoadConferences(Priority.UserInitiated), CanLoadConferences);
		}

		private ObservableCollection<Conference> _conferences;
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

		private async Task LoadConferences(Priority priority)
		{
				var conferences = await _conferencesService
    				.GetConferences(_settingsService.UserIdToken, priority)
					.ConfigureAwait(false);
	
				this.Conferences = new ObservableCollection<Conference> (conferences);
		}


		public bool CanLoadConferences()
		{
			return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
		}
	}

}