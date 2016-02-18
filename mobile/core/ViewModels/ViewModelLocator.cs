using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core.Services;

namespace TekConf.Mobile.Core.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<ConferencesListViewModel>();
			SimpleIoc.Default.Register<MyConferencesViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<ISettingsService, SettingsService> ();
			SimpleIoc.Default.Register<IConferencesService, ConferencesService> ();
			SimpleIoc.Default.Register<ISchedulesService, SchedulesService> ();
			SimpleIoc.Default.Register<IApiService, ApiService> ();

        }

		public MyConferencesViewModel MyConferences
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MyConferencesViewModel>();
			}
		}
		public ConferencesListViewModel Conferences
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ConferencesListViewModel>();
			}
		}

		public ConferenceDetailViewModel2 Conference { get; set; }
		public SessionDetailViewModel Session { get; set; }
		public SpeakerDetailViewModel Speaker { get; set; }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}