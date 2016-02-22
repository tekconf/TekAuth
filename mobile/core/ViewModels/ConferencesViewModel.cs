using MvvmCross.Core.ViewModels;
using Tekconf.DTO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TekConf.Mobile.Core.Services;
using Fusillade;
using System.Threading.Tasks;
using AutoMapper;

namespace TekConf.Mobile.Core.ViewModels
{
    public class ConferencesViewModel : MvxViewModel
	{
        public IMvxCommand ShowConference { get; private set; }
		public IMvxCommand LoadConferences { get; private set; }
		private readonly IMapper _mapper;

	    public ConferencesViewModel(IConferencesService conferencesService, IMapper mapper)
	    {
			_mapper = mapper;

			Conferences = new ObservableCollection<ConferenceListViewModel> ();
	        
			this.ShowConference = new MvxCommand<ConferenceListViewModel>((conference) =>
            {
                var navObject = new ConferenceDetailViewModel.NavObject()
                {
                    Slug = conference.Slug
                };
                ShowViewModel<ConferenceDetailViewModel>(navObject);
            });

			this.LoadConferences = new MvxCommand<Priority>(async (priority) =>
				{
					await Load(conferencesService, priority);
				});
        }

		private async Task Load(IConferencesService conferencesService, Priority priority)
		{
			var conferences = await conferencesService.GetConferences("", priority);
			var viewModels = _mapper.Map <List<ConferenceListViewModel>>(conferences);
			Conferences = new ObservableCollection<ConferenceListViewModel>(viewModels);
		}
			
		private ObservableCollection<ConferenceListViewModel> _conferences;
		public ObservableCollection<ConferenceListViewModel> Conferences
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
