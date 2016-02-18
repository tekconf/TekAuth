using MvvmCross.Core.ViewModels;
using Tekconf.DTO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TekConf.Mobile.Core.Services;
using Fusillade;
using System.Threading.Tasks;

namespace TekConf.Mobile.Core.ViewModels
{
    public class ConferencesViewModel : MvxViewModel
	{
        public IMvxCommand ShowConference { get; private set; }
		public IMvxCommand LoadConferences { get; private set; }

	    public ConferencesViewModel(IConferencesService conferencesService)
	    {
			Conferences = new ObservableCollection<Conference> ();
	        this.ShowConference = new MvxCommand<Conference>((conference) =>
            {
                var navObject = new ConferenceDetailViewModel.NavObject()
                {
                    Slug = conference.Slug
                };
                ShowViewModel<ConferenceDetailViewModel>(navObject);
            });

			this.LoadConferences = new MvxCommand(async () =>
				{
					await Load(conferencesService);
				});
        }

		private async Task Load(IConferencesService conferencesService)
		{
			var conferences = await conferencesService.GetConferences("", Priority.Explicit);
			Conferences = new ObservableCollection<Conference>(conferences);
		}
			
		private ObservableCollection<Conference> _conferences;
		public ObservableCollection<Conference> Conferences
		{
			get { 
				return _conferences;
			}
			set { 
				SetProperty (ref _conferences, value);
			}
		}
	}
    
}
