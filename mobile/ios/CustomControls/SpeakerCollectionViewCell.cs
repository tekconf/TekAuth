using Foundation;
using System;
using System.CodeDom.Compiler;
using SDWebImage;
using UIKit;
using Tekconf.DTO;

namespace ios
{
    public partial class SpeakerCollectionViewCell : UICollectionViewCell
    {
        private Speaker _speaker;
        public SpeakerCollectionViewCell (IntPtr handle) : base (handle)
        {
        }

		public void SetSpeaker(Speaker speaker)
		{
		    _speaker = speaker;

            this.speakerName.Text = speaker.FirstName + " " + speaker.LastName;
			SetImage();
		}

		private void SetImage()
		{
		    if (!string.IsNullOrWhiteSpace(_speaker.ImageUrl))
		    {
                speakerImage.SetImage(
                    url: new NSUrl(_speaker.ImageUrl),
                    placeholder: UIImage.FromBundle("BlankUser.png")
                );
            }

            double min = Math.Min(speakerImage.Frame.Width, speakerImage.Frame.Height);
            speakerImage.Layer.CornerRadius = (float)(min / 2.0);
            speakerImage.Layer.MasksToBounds = false;
            speakerImage.Layer.BorderColor = UIColor.Black.CGColor;
            speakerImage.Layer.BorderWidth = 3;
            speakerImage.ClipsToBounds = true;
        }
    }
}