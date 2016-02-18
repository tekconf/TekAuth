using MvvmCross.Core.ViewModels;
using Tekconf.DTO;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TekConf.Mobile.Core.ViewModels
{
    public class ConferencesViewModel : MvxViewModel
	{
        public IMvxCommand ShowConference { get; private set; }

	    public ConferencesViewModel()
	    {
	        this.ShowConference = new MvxCommand<Conference>((conference) =>
            {
                var navObject = new ConferenceDetailViewModel.NavObject()
                {
                    Slug = conference.Slug
                };
                ShowViewModel<ConferenceDetailViewModel>(navObject);
            });
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
