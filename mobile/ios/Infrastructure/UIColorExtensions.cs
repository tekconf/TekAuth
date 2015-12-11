using System;
using UIKit;
using System.Globalization;

namespace ios
{
	public static class UIColorExtensions
	{
		public static UIColor FromHex (string hexColor)
		{
			if (!string.IsNullOrWhiteSpace (hexColor)) {
				var red = int.Parse (hexColor.Substring (0, 2), NumberStyles.AllowHexSpecifier);
				var green = int.Parse (hexColor.Substring (2, 2), NumberStyles.AllowHexSpecifier);
				var blue = int.Parse (hexColor.Substring (4, 2), NumberStyles.AllowHexSpecifier);

				return UIColor.FromRGB (red, green, blue);

			} else {

				return UIColor.Green;
			}

//			return UIColor.FromRGB(
//				(((float)((hexValue & 0xFF0000) >> 16))/255.0f),
//				(((float)((hexValue & 0xFF00) >> 8))/255.0f),
//				(((float)(hexValue & 0xFF))/255.0f)
//			);
		}
	}
}