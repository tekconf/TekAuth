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
using MapKit;
using CoreLocation;

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

			viewSessions.Layer.BorderColor = UIColor.LightGray.CGColor;
			viewSessions.Layer.BorderWidth = 0.5f;

			this.Title = Vm.Conference.Name;

//			this.NavigationItem.SetRightBarButtonItem(
//				new UIBarButtonItem(UIImage.FromBundle("ConferenceAdd")
//					, UIBarButtonItemStyle.Plain
//					, (sender,args) => {
//						var button = sender as UIBarButtonItem;
//						button.Image = UIImage.FromBundle("ConferencesAdded");
//					})
//				, true);

			conferenceName.Text = Vm.Conference.Name;
			conferenceName.SizeToFit ();

			conferenceDescription.Text = Vm.Conference.Description;
			conferenceDescription.SizeToFit ();

			conferenceStartDate.Text = Vm.DateRange;
			conferenceStartDate.SizeToFit ();

			//highlightColor.BackgroundColor = UIColorExtensions.FromHex (Vm.Conference.HighlightColor);
			GetImage (Vm.Conference);
			ShowMap ();
		}

		private void ShowMap()
		{
			MKMapCamera currentLocation = new MKMapCamera {
				CenterCoordinate = new CLLocationCoordinate2D(latitude: 42.467051, longitude: -83.409285),
				Altitude = 1200.0,
				Pitch = 45.0f,
				Heading = 130.0
			};

			conferenceMap.SetCamera (currentLocation, animated: true);

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
