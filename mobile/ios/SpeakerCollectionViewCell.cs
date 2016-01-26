// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Tekconf.DTO;

namespace ios
{
    public partial class SpeakerCollectionViewCell : UICollectionViewCell
    {
        public SpeakerCollectionViewCell (IntPtr handle) : base (handle)
        {
        }

		public void SetSpeaker(Speaker speaker)
		{
			this.speakerName.Text = speaker.FirstName + " " + speaker.LastName;
			SetImage();
		}

		private void SetImage()
		{
			double min = Math.Min(speakerImage.Frame.Width, speakerImage.Frame.Height);
			speakerImage.Layer.CornerRadius = (float)(min / 2.0);
			speakerImage.Layer.MasksToBounds = false;
			speakerImage.Layer.BorderColor = UIColor.Black.CGColor;
			speakerImage.Layer.BorderWidth = 3;
			speakerImage.ClipsToBounds = true;
		}
    }
}