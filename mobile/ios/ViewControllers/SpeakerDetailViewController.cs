using System;
using Foundation;
using SDWebImage;
using Tekconf.DTO;
using UIKit;
using TekConf.Mobile.Core.ViewModels;

namespace ios
{
    partial class SpeakerDetailViewController : UIViewController
    {
        
        public SpeakerDetailViewController(IntPtr handle) : base(handle)
        {
        }

		private SpeakerDetailViewModel Vm
		{
			get
			{
				return Application.Locator.Speaker;
			}
		}
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

			this.NavigationController.NavigationBar.BarTintColor = UIColorExtensions.FromHex (Application.Locator.Conference.Conference.HighlightColor);
			this.Title = Vm.Speaker.FirstName + " " + Vm.Speaker.LastName;
            speakerName.Text = Vm.Speaker.FirstName + " " + Vm.Speaker.LastName;
            speakerBio.Text = Vm.Speaker.Bio;
            speakerTwitterHandle.SetTitle(Vm.Speaker.TwitterHandle, UIControlState.Normal);
            speakerTwitterHandle.TouchUpInside += ShowSpeakerTwitterProfile;
            speakerCompany.Text = Vm.Speaker.CompanyName;

            SetImage();
        }

        private void ShowSpeakerTwitterProfile(object sender, EventArgs eventArgs)
        {
			string screenName = Vm.Speaker.TwitterHandle;
			if (Vm.Speaker.TwitterHandle.StartsWith ("@")) {
				screenName = Vm.Speaker.TwitterHandle.Substring (1);
			}
			var nativeTwitterUrl = new NSUrl($@"twitter://user?screen_name={screenName}");
            var webTwitterUrl = new NSUrl($@"http://twitter.com/{screenName}");

            UIApplication.SharedApplication.OpenUrl(
                UIApplication.SharedApplication.CanOpenUrl(nativeTwitterUrl)
                ? nativeTwitterUrl
                : webTwitterUrl
            );
        }

        private void SetImage()
        {
            if (!string.IsNullOrWhiteSpace(Vm.Speaker.ImageUrl))
            {
                speakerImage.SetImage(
                    url: new NSUrl(Vm.Speaker.ImageUrl),
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
