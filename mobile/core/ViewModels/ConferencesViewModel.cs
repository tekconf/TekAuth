using MvvmCross.Core.ViewModels;
using Tekconf.DTO;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TekConf.Mobile.Core.ViewModels
{
	public class ConferencesViewModel : MvxViewModel
	{
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
