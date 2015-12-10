using UIKit;
using TekConf.Mobile.Core.ViewModel;

namespace ios
{
	public class Application
	{
		private static ViewModelLocator _locator;
		public static ViewModelLocator Locator
		{
			get
			{
				return _locator ?? (_locator = new ViewModelLocator());
			}
		}
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			//Xamarin.Insights.Initialize ("7259550172eb1dfb3a35d38595f8d3f08ff6c7b0");
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}