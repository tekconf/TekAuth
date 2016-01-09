using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace TekConf.Mobile.Core.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<ConferencesViewModel>();
			SimpleIoc.Default.Register<MyConferencesViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<ISettingsService, SettingsService> ();
			SimpleIoc.Default.Register<IConferencesService, ConferencesService> ();
			SimpleIoc.Default.Register<IApiService, ApiService> ();

        }

		public MyConferencesViewModel MyConferences
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MyConferencesViewModel>();
			}
		}
		public ConferencesViewModel Conferences
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ConferencesViewModel>();
			}
		}

		public ConferenceDetailViewModel Conference { get; set; }
		public SessionDetailViewModel Session { get; set; }

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