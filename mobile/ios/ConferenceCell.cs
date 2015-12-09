using System;
using UIKit;
using Tekconf.DTO;
using CoreGraphics;
using System.Globalization;
using System.Net.Http;
using ModernHttpClient;
using System.IO;
using System.Threading.Tasks;

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
			highlightColorBar.BackgroundColor = UIColorExtensions.FromHex (conference.HighlightColor);


			if (conference.StartDate.HasValue) {
				this.conferenceDate.Text = conference.StartDate.Value.ToShortDateString ();
			}
			this.conferenceDescription.Text = conference.Description;

			this.conferenceLocation.Text = "San Francisco, CA";

			if (!string.IsNullOrWhiteSpace (conference.ImageUrl)) {
				try {
					await DownloadAsync (conference);
				}
				catch (Exception e) {
					var sdsd = e.Message;
				}
			}
		}


		async Task DownloadAsync(Conference conference)
		{

			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string localFilename = conference.Slug + ".png";
			string localPath = Path.Combine (documentsPath, localFilename);
			byte[] bytes = null;

			//if (!File.Exists (localPath)) {
				using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

					try {
						bytes = await httpClient.GetByteArrayAsync (conference.ImageUrl);
					} catch (OperationCanceledException) {
						Console.WriteLine ("Task Canceled!");
						return;
					} catch (Exception e) {
						Console.WriteLine (e.ToString ());
						return;
					}
					//Save the image using writeAsync
					FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
					await fs.WriteAsync (bytes, 0, bytes.Length);
				}
			//} 

				//Resizing image is time costing, using async to avoid blocking the UI thread
				UIImage image = null;
				CGSize imageViewSize = conferenceImage.Frame.Size;

				await Task.Run (() => {
					image = UIImage.FromFile (localPath).Scale (imageViewSize);
				});


				conferenceImage.Image = image;

			
		}

	}

	public static class UIColorExtensions
	{
		public static UIColor FromHex(string hexColor)
		{
			if (!string.IsNullOrWhiteSpace (hexColor)) {
				var red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
				var green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
				var blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);

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