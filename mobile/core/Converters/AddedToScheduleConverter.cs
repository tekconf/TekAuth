using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace TekConf.Mobile.Core
{
	public class AddedToScheduleConverter : MvxValueConverter<bool, string>
	{
		protected override string Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return value ? "\xf274" : "\xf273";           
		}
	}
}


