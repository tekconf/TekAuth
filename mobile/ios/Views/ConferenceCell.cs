using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Tekconf.DTO;

namespace ios
{
	public partial class ConferenceCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString ("ConferenceCell");
		public static readonly UINib Nib;

		//static ConfListCell ()
		//{
		//	Nib = UINib.FromName ("ConfListCell", NSBundle.MainBundle);
		//}

		//public ConfListCell (IntPtr handle) : base (handle)
		//{
		//}
		private const string BindingText = "name Name;";

		public ConferenceCell()
			: base(BindingText)
		{

		}

		//public ConferenceCell(IntPtr handle)
		//	: base(BindingText, handle)
		//{

		//}

		public ConferenceCell(IntPtr handle)
			: base(handle)
		{
			this.DelayBind (() => {
				var set = this.CreateBindingSet<ConferenceCell, Conference> ();
				set.Bind (name).To (item => item.Name);
				set.Bind (description).To (item => item.Description);
				//set.Bind(_imageViewLoader).To (item => item.ImageSquareUrl);
				//set.Bind (AuthorLabel).To (item => item.volumeInfo.authorSummary);
				//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
				set.Apply ();
			});
		}

		public static float GetCellHeight()
		{
			return 221f;
		}
//		public static readonly NSString Key = new NSString ("ConferenceCell");
//		public static readonly UINib Nib;
//		//private readonly MvxImageViewLoader _imageViewLoader;
//		static ConferenceCell ()
//		{
//			Nib = UINib.FromName ("ConferenceCell", NSBundle.MainBundle);
//		}

//		public ConferenceCell (IntPtr handle) : base (handle)
//		{
////			_imageViewLoader = new MvxImageViewLoader(() => this.image);
////
////			this.DelayBind (() => {
////				var set = this.CreateBindingSet<ConferenceCell, Conference> ();
////				set.Bind (name).To (item => item.Name);
////				set.Bind (description).To (item => item.Description);
////				set.Bind(_imageViewLoader).To (item => item.ImageSquareUrl);
////				//set.Bind (AuthorLabel).To (item => item.volumeInfo.authorSummary);
////				//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
////				set.Apply ();
////			});
//		}
	}
}