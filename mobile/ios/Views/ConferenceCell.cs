using System;
using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TekConf.Mobile.Core.ViewModels;

namespace ios
{
	public partial class ConferenceCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("ConferenceCell");

		private MvxImageViewLoader _imageViewLoader;

		public ConferenceCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() => {
			   contentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			   contentView.Layer.BorderWidth = 0.5f;

			   _imageViewLoader = new MvxImageViewLoader(() => this.image);
			   this.scheduleStatus.Font = UIFont.FromName("FontAwesome", 17f);

			   var set = this.CreateBindingSet<ConferenceCell, ConferenceListViewModel>();
			   //set.Bind(AccessibilityIdentifier).To(conf => conf.Slug);

			   set.Bind(name).To(conf => conf.Name);
			   set.Bind(description).To(conf => conf.Description);
			   set.Bind(date).To(conf => conf.ShortDate);
			   set.Bind(location).To(conf => conf.Location);
			   set.Bind(scheduleStatus).To(conf => conf.IsAddedToSchedule).WithConversion("AddedToSchedule", null);

			   set.Bind(highlightColor).For(highlightColor => highlightColor.BackgroundColor).To(conf => conf.HighlightColor).WithConversion("RGBA");
			   set.Bind(favoriteView).For(favoriteView => favoriteView.BackgroundColor).To(conf => conf.HighlightColor).WithConversion("RGBA");

			   set.Bind(_imageViewLoader).To(item => item.ImageUrl);
			   set.Apply();
		   });
		}
	}
}