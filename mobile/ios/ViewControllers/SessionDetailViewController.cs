using System;
using UIKit;
using TekConf.Mobile.Core.ViewModels;
using Microsoft.Practices.ServiceLocation;
using TekConf.Mobile.Core.Services;
using Foundation;
using System.Linq;

namespace ios
{
    partial class SessionDetailViewController : UIViewController, IUICollectionViewDataSource, IUICollectionViewDelegate
    {
        public SessionDetailViewController(IntPtr handle) : base(handle)
        {
        }

        private SessionDetailViewModel Vm
        {
            get
            {
                return Application.Locator.Session;
            }
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            speakersCollectionView.WeakDataSource = this;
            //speakersList.TableFooterView = new UIView();
            addToMySchedule.Layer.BorderColor = UIColor.LightGray.CGColor;
            addToMySchedule.Layer.BorderWidth = 0.5f;

            //if (Vm.Session.IsAddedToSchedule)
            //{
            //	SetRemoveButtonStatus();
            //}
            //else
            //{
            SetAddButtonStatus();
            //}
            addToMySchedule.TouchUpInside += (sender, e) =>
            {
                var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
                if (!string.IsNullOrWhiteSpace(settingsService.UserIdToken))
                {
                    SetRemoveButtonStatus();
                }
                else {
                    new UIAlertView("Login", "You must login to add a session to your schedule", null, "Ok", null).Show();
                }
            };

            sessionTitle.Text = Vm.Title ?? string.Empty;
            sessionDescription.Text = Vm.Description ?? string.Empty;
            sessionRoom.Text = Vm.Room ?? string.Empty;
            sessionTime.Text = Vm.DateRange ?? string.Empty;
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
                Font = UIFont.FromName("FontAwesome", 16f)
            };


            var textAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.FromRGBA(red: 0f, blue: 1.0f, green: 0.478431f, alpha: 1.0f),
                Font = UIFont.FromName("OpenSans-Light", 16f)
            };

            var prettyString = new NSMutableAttributedString(status);
            prettyString.SetAttributes(statusAttributes.Dictionary, new NSRange(0, 1));
            prettyString.SetAttributes(textAttributes.Dictionary, new NSRange(2, 17));
            this.addToMySchedule.SetAttributedTitle(prettyString, UIControlState.Normal);
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (this.Vm == null
                || this.Vm.Session == null
                || this.Vm.Session.Speakers == null)
            {
                return 0;
            }

            return this.Vm.Session.Speakers.Count();
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {

            var speaker = this.Vm.Session.Speakers.ToArray()[indexPath.Row];
            SpeakerCollectionViewCell cell = (SpeakerCollectionViewCell)collectionView.DequeueReusableCell("speakersCollectionCell", indexPath);
            cell.SetSpeaker(speaker);

            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var indexPaths = speakersCollectionView.GetIndexPathsForSelectedItems();
            if (indexPaths == null || !indexPaths.Any()) return;

            var speaker = Vm.Session.Speakers[indexPaths.First().Row];
            var vc = segue.DestinationViewController as SpeakerDetailViewController;
            vc?.SetSpeaker(speaker);

        }

    }
}