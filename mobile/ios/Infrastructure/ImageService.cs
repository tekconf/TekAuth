using System;
using UIKit;
using System.Globalization;
using TekConf.Mobile.Core;
using System.Threading.Tasks;
using Tekconf.DTO;
using System.IO;
using System.Net.Http;
using ModernHttpClient;
using TekConf.Mobile.Core.Services;
using Xamarin;

namespace ios
{
	public class ImageService : IImageService
	{
		public async Task<string> GetConferenceImagePath (Conference conference)
		{

			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string localFilename = conference.Slug + ".png";
			string localPath = Path.Combine (documentsPath, localFilename);
			byte[] bytes = null;

			if (!File.Exists (localPath)) {
				using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

					try {
						bytes = await httpClient.GetByteArrayAsync (conference.ImageUrl);
					} catch (OperationCanceledException opEx) {
						Insights.Report (opEx);
						return null;
					} catch (Exception e) {
						Insights.Report (e);
						return null;
					}

					//Save the image using writeAsync
					FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
					await fs.WriteAsync (bytes, 0, bytes.Length);
				}
			} 

			return localPath;

		}

		public async Task<string> GetSpeakerImagePath (Conference conference, Speaker speaker)
		{

			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string localFilename = conference.Slug + "-" + speaker.Slug + ".png";
			string localPath = Path.Combine (documentsPath, localFilename);
			byte[] bytes = null;

			if (!File.Exists (localPath)) {
				using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

					try {
						bytes = await httpClient.GetByteArrayAsync (conference.ImageUrl);
					} catch (OperationCanceledException opEx) {
						Insights.Report (opEx);
						return null;
					} catch (Exception e) {
						Insights.Report (e);
						return null;
					}

					//Save the image using writeAsync
					FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
					await fs.WriteAsync (bytes, 0, bytes.Length);
				}
			} 

			return localPath;

		}

	}
	
}