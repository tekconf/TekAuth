using UIKit;
using TekConf.Mobile.Core.ViewModels;

namespace ios
{
	public class Application
	{
		private static ViewModelLocator _locator;
		public static ViewModelLocator Locator
		{
			get
			{
				return _locator ?? (_locator = new ViewModelLocator().AddPlatformSpecificImplementations());
			}
		}

		static void Main (string[] args)
		{
			Xamarin.Insights.Initialize ("7259550172eb1dfb3a35d38595f8d3f08ff6c7b0");

			UIApplication.Main (args, null, "AppDelegate");
		}
	}

}