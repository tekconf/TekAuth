using GalaSoft.MvvmLight;
using System;
using Tekconf.DTO;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Fusillade;
using System.Threading.Tasks;
using TekConf.Mobile.Core.Services;

namespace TekConf.Mobile.Core.ViewModels
{
	public class ConferenceDetailViewModel2 : ViewModelBase
	{
		public Conference Conference { get; private set;}
		public ICommand AddToScheduleCommand { get; private set; }
		public ICommand RemoveFromScheduleCommand { get; private set; }

        private readonly ISchedulesService _schedulesService;
		private readonly ISettingsService _settingsService;

		public ConferenceDetailViewModel2 (Conference conference, ISchedulesService schedulesService, ISettingsService settingsService)
		{
			Conference = conference;
			_schedulesService = schedulesService;
			_settingsService = settingsService;

            this.AddToScheduleCommand = new RelayCommand(async () => await this.AddToSchedule(Priority.UserInitiated), CanAddToSchedule);
            this.RemoveFromScheduleCommand = new RelayCommand(async () => await this.RemoveFromSchedule(Priority.UserInitiated), CanRemoveFromSchedule);
        }

        private async Task AddToSchedule(Priority priority)
		{
			var schedule = await _schedulesService
				.AddToSchedule(priority, Conference.Slug)
				.ConfigureAwait(false);

            this.Conference.IsAddedToSchedule = true;
        }

        public bool CanAddToSchedule()
		{
			return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
		}

        private async Task RemoveFromSchedule(Priority priority)
        {
            await _schedulesService
                .RemoveFromSchedule(priority, Conference.Slug)
                .ConfigureAwait(false);

            this.Conference.IsAddedToSchedule = false;
        }

        public bool CanRemoveFromSchedule()
        {
            return !string.IsNullOrWhiteSpace(_settingsService.UserIdToken);
        }

        public string DateRange
		{
			get
			{

				string range;
				if (Conference.StartDate == default(DateTime?) && Conference.EndDate == default(DateTime?)) {
					range = "No Date Set";
				} else if (Conference.StartDate.HasValue && !Conference.EndDate.HasValue) {
					// Only start Date
					range = Conference.StartDate.Value.ToString("MMMM") + " " + Conference.StartDate.Value.Day + ", " + Conference.StartDate.Value.Year;
				}
				else if (Conference.StartDate.Value.Month == Conference.EndDate.Value.Month && Conference.StartDate.Value.Year == Conference.EndDate.Value.Year)
				{
					// They begin and end in the same month
					if (Conference.StartDate.Value.Date == Conference.EndDate.Value.Date)
					{
						range = Conference.StartDate.Value.ToString("MMMM") + " " + Conference.StartDate.Value.Day + ", " + Conference.StartDate.Value.Year;
					}
					else
						range = Conference.StartDate.Value.ToString("MMMM") + " " + Conference.StartDate.Value.Day + " - " + Conference.EndDate.Value.Day + ", " + Conference.StartDate.Value.Year;
				}
				else
				{
					// They begin and end in different months
					if (Conference.StartDate.Value.Year == Conference.EndDate.Value.Year)
					{
						range = Conference.StartDate.Value.ToString("MMMM") + " " + Conference.StartDate.Value.Day + " - " + Conference.EndDate.Value.ToString("MMMM") + " " + Conference.EndDate.Value.Day + ", " + Conference.StartDate.Value.Year;
					}
					else
					{
						range = Conference.StartDate.Value.ToString("MMMM") + " " + Conference.StartDate.Value.Day + ", " + Conference.StartDate.Value.Year + " - " + Conference.EndDate.Value.ToString("MMMM") + " " + Conference.EndDate.Value.Day + ", " + Conference.EndDate.Value.Year;
					}

				}

				return range;
			}
		}

    }
}