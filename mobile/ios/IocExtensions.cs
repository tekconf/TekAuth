using UIKit;
using TekConf.Mobile.Core.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using TekConf.Mobile.Core;

namespace ios
{

	public static class IocExtensions
	{
		public static ViewModelLocator AddPlatformSpecificImplementations(this ViewModelLocator viewModelLocator)
		{
			SimpleIoc.Default.Register<IImageService, ImageService> ();

			return viewModelLocator;

		}
	}
}