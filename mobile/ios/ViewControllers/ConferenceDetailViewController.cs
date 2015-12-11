using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core;
using CoreGraphics;
using System.Threading.Tasks;

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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_nameBinding = this.SetBinding (
				() => Vm.Name,
				() => conferenceName.Text);

			_descriptionBinding = this.SetBinding (
				() => Vm.Description,
				() => conferenceDescription.Text);

			_startDateBinding = this.SetBinding (
				() => Vm.StartDate,
				() => conferenceStartDate.Text);

			_endDateBinding = this.SetBinding (
				() => Vm.EndDate,
				() => conferenceEndDate.Text);

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
					var sdsd = e.Message;
				}
			}
		}
	}
}
