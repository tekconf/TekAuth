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
            SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<ISettingsService, SettingsService> ();
        }

		public ConferencesViewModel Conferences
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ConferencesViewModel>();
			}
		}

		public ConferenceDetailViewModel Conference { get; set; }

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