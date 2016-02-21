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

		private const string BindingText = "name Name;";
		private MvxImageViewLoader _imageViewLoader;

		public ConferenceCell() : base(BindingText)
		{
		}


		public ConferenceCell(IntPtr handle) : base(handle)
		{
			

			this.DelayBind (() =>
			{
				_imageViewLoader = new MvxImageViewLoader(() => this.image);
				this.scheduleStatus.Font = UIFont.FromName("FontAwesome", 17f);

				var vm = this.DataContext as Conference;
				if (vm != null) {
					this.AccessibilityIdentifier = vm.Slug;
				}
				var set = this.CreateBindingSet<ConferenceCell, Conference> ();
				set.Bind (name).To (item => item.Name);
				set.Bind (description).To (item => item.Description);
				//set.Bind (location).To (item => "San Francisco, CA");
				//set.Bind(highlightColor).For(v => v.BackgroundColor).To(item => UIColorExtensions.FromHex (item.HighlightColor));
				//set.Bind(favoriteView).For(v => v.BackgroundColor).To(item => UIColorExtensions.FromHex (item.HighlightColor));
				//set.Bind (scheduleStatus).To (item => item.Description);

				set.Bind(_imageViewLoader).To (item => item.ImageUrl);
				//set.Bind (AuthorLabel).To (item => item.volumeInfo.authorSummary);
				//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
				set.Apply ();
			});
		}

		////		public void SetConference (Conference conference)
		////		{
		////			conferenceContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
		////			conferenceContentView.Layer.BorderWidth = 0.5f;
		////
		////			this.conferenceName.Text = conference.Name;
		////			this.AccessibilityIdentifier = conference.Slug;
		////
		////			highlightColorBar.BackgroundColor = UIColorExtensions.FromHex (conference.HighlightColor);
		////			conferenceFavoriteView.BackgroundColor = UIColorExtensions.FromHex (conference.HighlightColor);
		////
		////			if (conference.StartDate.HasValue) {
		////				this.conferenceDate.Text = conference.StartDate.Value.ToShortDateString ();
		////			}
		////			this.conferenceDescription.Text = conference.Description;
		////
		////			this.conferenceLocation.Text = conference.Address.AddressShortDisplay();
		////
		////			this.addedToScheduleStatus.Font = UIFont.FromName("FontAwesome", 17f);
		////            this.addedToScheduleStatus.Text = conference.IsAddedToSchedule ? "\xf274" : "\xf273"; //
		////            //this.addedToScheduleStatus.Text = "\xf273";
		////
		//////            if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
		//////				try {
		//////					
		//////					var imageService = ServiceLocator.Current.GetInstance<IImageService>();
		//////					var localPath = await imageService.GetConferenceImagePath(conference);
		//////
		//////					//Resizing image is time costing, using async to avoid blocking the UI thread
		//////					UIImage image = null;
		//////					CGSize imageViewSize = conferenceImage.Frame.Size;
		//////
		//////					await Task.Run (() => {
		//////						var uiImage = UIImage.FromFile (localPath);
		//////						image = uiImage?.Scale(imageViewSize);
		//////					});
		//////
		//////
		//////					conferenceImage.Image = image;
		//////
		//////				} catch (Exception e) {
		//////					Insights.Report (e);
		//////				}
		//////			}

		//public static float GetCellHeight()
		//{
		//	return 221f;
		//}
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