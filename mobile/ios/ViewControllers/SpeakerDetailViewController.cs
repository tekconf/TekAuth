using System;
using Foundation;
using SDWebImage;
using Tekconf.DTO;
using UIKit;

namespace ios
{
    partial class SpeakerDetailViewController : UIViewController
    {
        private Speaker _speaker;
        public SpeakerDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public void SetSpeaker(Speaker speaker)
        {
            _speaker = speaker;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            speakerName.Text = _speaker.FirstName + " " + _speaker.LastName;
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
