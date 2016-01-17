using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Tekconf.DTO;
using Fusillade;
using System.Linq;
using TekConf.Mobile.Core.Services;

namespace TekConf.Mobile.Core.ViewModels
{
	public class MyConferencesViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly ISchedulesService _schedulesService;
        public RelayCommand LoadSchedulesCommand { get; private set; }

        public MyConferencesViewModel (ISettingsService settingsService, ISchedulesService schedulesService)
		{
			_schedulesService = schedulesService;
			_settingsService = settingsService;
			_myConferences = new ObservableCollection<Conference> ();
			this.LoadSchedulesCommand = new RelayCommand(async () => await this.LoadSchedules(Priority.UserInitiated), CanLoadSchedules);
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

		public async Task LoadSchedules(Priority priority)
		{
			var schedules = await _schedulesService
				.GetSchedules(priority)
				.ConfigureAwait(false);

			var conferences = schedules.Select(s => s.Conference).ToList();
			this.MyConferences = new ObservableCollection<Conference> (conferences);
		}


		public bool CanLoadSchedules()
		{
			return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
		}
	}

}