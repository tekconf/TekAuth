using UIKit;
using TekConf.Mobile.Core.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using TekConf.Mobile.Core;
using TekConf.Mobile.Core.Services;

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