using System;
using UIKit;
using Tekconf.DTO;
using CoreGraphics;
using System.Globalization;
using System.Net.Http;
using ModernHttpClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core;

namespace ios
{
	partial class ConferenceCell : UITableViewCell
	{
		public ConferenceCell (IntPtr handle) : base (handle)
		{
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			conferenceContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			conferenceContentView.Layer.BorderWidth = 0.5f;
		}

		public async Task SetConference (Conference conference)
		{
			this.conferenceName.Text = conference.Name;
			this.AccessibilityIdentifier = conference.Slug;

			highlightColorBar.BackgroundColor = UIColorExtensions.FromHex (conference.HighlightColor);
			conferenceFavoriteView.BackgroundColor = UIColorExtensions.FromHex (conference.HighlightColor);

			if (conference.StartDate.HasValue) {
				this.conferenceDate.Text = conference.StartDate.Value.ToShortDateString ();
			}
			this.conferenceDescription.Text = conference.Description;

			this.conferenceLocation.Text = "San Francisco, CA";

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


//		async Task DownloadAsync (Conference conference)
//		{
//
//			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//			string localFilename = conference.Slug + ".png";
//			string localPath = Path.Combine (documentsPath, localFilename);
//			byte[] bytes = null;
//
//			if (!File.Exists (localPath)) {
//				using (var httpClient = new HttpClient (new NativeMessageHandler ())) {
//
//					try {
//						bytes = await httpClient.GetByteArrayAsync (conference.ImageUrl);
//					} catch (OperationCanceledException) {
//						Console.WriteLine ("Task Canceled!");
//						return;
//					} catch (Exception e) {
//						Console.WriteLine (e.ToString ());
//						return;
//					}
//					//Save the image using writeAsync
//					FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
//					await fs.WriteAsync (bytes, 0, bytes.Length);
//				}
//			} 
//
//			//Resizing image is time costing, using async to avoid blocking the UI thread
//			UIImage image = null;
//			CGSize imageViewSize = conferenceImage.Frame.Size;
//
//			await Task.Run (() => {
//				image = UIImage.FromFile (localPath).Scale (imageViewSize);
//			});
//
//
//			conferenceImage.Image = image;
//
//			
//		}

	}

}