using System;
using UIKit;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModels;
using Microsoft.Practices.ServiceLocation;
using CoreGraphics;
using System.Threading.Tasks;
using Xamarin;
using System.Collections.Generic;
using MapKit;
using CoreLocation;
using System.Linq;
using Foundation;
using GalaSoft.MvvmLight.Messaging;
using TekConf.Mobile.Core.Messages;
using TekConf.Mobile.Core.Services;

namespace ios
{
	partial class ConferenceDetailViewController : UIViewController
	{
		CLLocationManager _locationManager;
		public ConferenceDetailViewController (IntPtr handle) : base (handle)
		{
		}

		private ConferenceDetailViewModel Vm {
			get {
				return Application.Locator.Conference;
			}
		}

		//private Binding<string, string> _nameBinding;
		//private Binding<string, string> _descriptionBinding;
		//private Binding<DateTime?, string> _startDateBinding;
		//private Binding<DateTime?, string> _endDateBinding;

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

			_locationManager = new CLLocationManager { DesiredAccuracy = 1000 };
			_locationManager.RequestWhenInUseAuthorization ();

		    if (Vm.Conference.IsAddedToSchedule)
		    {
                SetRemoveButtonStatus();
		    }
		    else
		    {
		        SetAddButtonStatus();
            }

			addToMySchedule.Layer.BorderColor = UIColor.LightGray.CGColor;

			addToMySchedule.Layer.BorderWidth = 0.5f;
			addToMySchedule.TouchUpInside += (sender, e) => {
                
				var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
				if (!string.IsNullOrWhiteSpace(settingsService.UserIdToken)) {
				    if (Vm.Conference.IsAddedToSchedule)
				    {
	                    Vm.RemoveFromScheduleCommand.Execute(null);
	                }
	                else
				    {
	                    Vm.AddToScheduleCommand.Execute(null);
	                }
				} else {
					new UIAlertView("Login", "You must login to add a conference to your schedule", null, "Ok", null).Show();
				}

            };

            Messenger.Default.Register<ConferenceAddedToScheduleMessage>
            (
                this,
				(message) => SetRemoveButtonStatus ()
            );

            Messenger.Default.Register<ConferenceRemovedFromScheduleMessage>
            (
                this,
				(message) => SetAddButtonStatus ()
            );

			viewSessions.Layer.BorderColor = UIColor.LightGray.CGColor;
			viewSessions.Layer.BorderWidth = 0.5f;

			var sessionCount = Vm?.Conference?.Sessions?.Count ();
			viewSessions.SetTitle ($"View {sessionCount} Sessions", UIControlState.Normal);

			this.Title = Vm.Conference.Name;

			conferenceName.Text = Vm.Conference.Name;
			conferenceName.SizeToFit ();

			conferenceDescription.Text = Vm.Conference.Description;
			conferenceDescription.SizeToFit ();

			conferenceStartDate.Text = Vm.DateRange;
			conferenceStartDate.SizeToFit ();

			conferenceAddress.Text = Vm.Conference.Address.AddressLongDisplay ();
			conferenceAddress.SizeToFit ();

			GetImage (Vm.Conference);
			ShowMap ();
		}

	    private const string AddToScheduleMessage = "\xf274 Add to My Schedule";
	    private const string RemoveFromScheduleMessage = "\xf273 Remove from My Schedule";

        private void SetAddButtonStatus()
	    {
	        SetButtonStatus(AddToScheduleMessage);
	    }

        private void SetRemoveButtonStatus()
        {
            SetButtonStatus(RemoveFromScheduleMessage);
        }

        private void SetButtonStatus(string status)
	    {
            var statusAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.FromRGBA(red: 0f, blue: 1.0f, green: 0.478431f, alpha: 1.0f),
                Font = UIFont.FromName("FontAwesome", 17f)
            };


            var textAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.FromRGBA(red: 0f, blue: 1.0f, green: 0.478431f, alpha: 1.0f),
                Font = UIFont.FromName("Open Sans Light", 17f)
            };

            var prettyString = new NSMutableAttributedString(status);
            prettyString.SetAttributes(statusAttributes.Dictionary, new NSRange(0, 1));
            prettyString.SetAttributes(textAttributes.Dictionary, new NSRange(2, 17));
            this.addToMySchedule.SetAttributedTitle(prettyString, UIControlState.Normal);
        }

        private void ShowMap()
		{
			if (Vm.Conference.Address.Latitude.HasValue && Vm.Conference.Address.Longitude.HasValue) {
				conferenceMap.Hidden = false;

				MKMapCamera currentLocation = new MKMapCamera {
					CenterCoordinate = new CLLocationCoordinate2D ((double)Vm.Conference.Address.Latitude.Value, (double)Vm.Conference.Address.Longitude.Value),
					Altitude = 1200.0,
					Pitch = 45.0f,
					Heading = 130.0
				};

				conferenceMap.SetCamera (currentLocation, animated: true);
			} else {
				//TODO : Look for address?
				conferenceMap.Hidden = true;
			}
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
