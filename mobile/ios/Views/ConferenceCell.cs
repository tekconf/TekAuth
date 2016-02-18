using System;

using Foundation;
using UIKit;
using Tekconf.DTO;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace ios
{
	public partial class ConferenceCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString ("ConferenceCell");
		public static readonly UINib Nib;

		static ConferenceCell ()
		{
			Nib = UINib.FromName ("ConferenceCell", NSBundle.MainBundle);
		}

		public ConferenceCell (IntPtr handle) : base (handle)
		{
			this.DelayBind(() => {
				var set = this.CreateBindingSet<ConferenceCell, Conference> ();
				set.Bind(name).To (item => item.Name);
				//set.Bind (AuthorLabel).To (item => item.volumeInfo.authorSummary);
				//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
				set.Apply();
			});
		}
	}
}
