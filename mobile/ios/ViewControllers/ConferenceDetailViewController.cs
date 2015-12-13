using System;
using UIKit;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core;
using CoreGraphics;
using System.Threading.Tasks;
using Xamarin;
using System.Collections.Generic;

namespace ios
{

	partial class ConferenceDetailViewController : UIViewController
	{
		public ConferenceDetailViewController (IntPtr handle) : base (handle)
		{
		}

		private ConferenceDetailViewModel Vm {
			get {
				return Application.Locator.Conference;
			}
		}

		private Binding<string, string> _nameBinding;
		private Binding<string, string> _descriptionBinding;
		private Binding<DateTime?, string> _startDateBinding;
		private Binding<DateTime?, string> _endDateBinding;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Insights.Track("ViewedScreen", 
				new Dictionary <string,string> { 
					{"Screen", "ConferenceDetail"},
					{"Slug", Vm.Conference.Slug }, 
				});
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			addToMySchedule.Layer.BorderColor = UIColor.LightGray.CGColor;
			addToMySchedule.Layer.BorderWidth = 0.5f;

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("ConferenceAdd")
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						var button = sender as UIBarButtonItem;
						button.Image = UIImage.FromBundle("ConferencesAdded");
					})
				, true);

			_nameBinding = this.SetBinding (
				() => Vm.Name,
				() => conferenceName.Text);

			_descriptionBinding = this.SetBinding (
				() => Vm.Description,
				() => conferenceDescription.Text);

			_startDateBinding = this.SetBinding (
				() => Vm.StartDate,
				() => conferenceStartDate.Text);
			
			_startDateBinding.ConvertSourceToTarget ((source) => source.HasValue ? source.Value.ToString("ddd, MMMM dd, yyyy") : string.Empty);
			_startDateBinding.ForceUpdateValueFromSourceToTarget ();
			_endDateBinding = this.SetBinding (
				() => Vm.EndDate,
				() => conferenceEndDate.Text);

			_endDateBinding.ConvertSourceToTarget ((source) => source.HasValue ? source.Value.ToShortDateString() : string.Empty);
			_endDateBinding.ForceUpdateValueFromSourceToTarget ();

			highlightColor.BackgroundColor = UIColorExtensions.FromHex (Vm.Conference.HighlightColor);
			GetImage (Vm.Conference);

//			_emailLabelBinding = this.SetBinding (
//				() => Vm.Email,
//				() => email.Text);
		}
		private async void GetImage(Conference conference)
		{
			if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
				try {

					var imageService = ServiceLocator.Current.GetInstance<IImageService>();
					var localPath = await imageService.GetImagePath(conference);

					//Resizing image is time costing, using async to avoid blocking the UI thread
					UIImage image = null;
					CGSize imageViewSize = conferenceImage.Frame.Size;

					await Task.Run (() => {
						image = UIImage.FromFile (localPath).Scale (imageViewSize);
					});


					conferenceImage.Image = image;

				} catch (Exception e) {
					Insights.Report (e);
				}
			}
		}
	}
}