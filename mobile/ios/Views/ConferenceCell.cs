using System;
using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TekConf.Mobile.Core.ViewModels;
namespace ios
{
	[Register("ConferenceCell")]
	public class ConferenceCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("ConferenceCell");

		public ConferenceCell()            
		{
			CreateLayout();
			InitializeBindings();
		}

		public ConferenceCell(IntPtr handle)
			: base(handle)
		{
			CreateLayout();
			InitializeBindings();
		}

		private UIView contentWrapperView;
		private UIView highlightColor;
		private UIImageView image;
		private UILabel name;
		private UILabel description;
		private UIView dateView;
		private UILabel date;
		private UIView locationView;
		private UILabel location;
		private UIView scheduleStatusView;
		private UILabel scheduleStatus;
		//static readonly nint widthPadding = 16;

		private void CreateLayout()
		{
			
			//ContentView
			//HighlightColor
			//Image
			//Name
			//Description
			//DateView
			//Date
			//LocationView
			//Location
			//ScheduleStatusView
			//ScheduleStatus
			this.BackgroundColor = UIColor.Yellow;
			contentWrapperView = new UIView()
			{
				BackgroundColor = UIColor.Red
			};
			this.ContentView.Add(contentWrapperView);

			highlightColor = new UIView { 
				BackgroundColor = UIColor.Green
			};

			contentWrapperView.Add(highlightColor);
			//this.TranslatesAutoresizingMaskIntoConstraints = false;
			//this.ContentView.TranslatesAutoresizingMaskIntoConstraints = false;

			this.ContentView.ConstrainLayout(() =>
			                                 contentWrapperView.Frame.Width == ContentView.Frame.Width - 16f &&
			                                 contentWrapperView.Frame.Right == ContentView.Frame.Right - 8f &&
			                                 contentWrapperView.Frame.Left == ContentView.Frame.Left + 8f &&
			                                 contentWrapperView.Frame.Height == ContentView.Frame.Height - 16f &&
			                                 contentWrapperView.Frame.Top == ContentView.Frame.Top + 8f &&
			                                 contentWrapperView.Frame.Bottom == ContentView.Frame.Bottom - 8f &&

			                                 highlightColor.Frame.Width == 8f &&
			                                 highlightColor.Frame.Left == contentWrapperView.Frame.Left &&
			                                 highlightColor.Frame.Height == contentWrapperView.Frame.Height &&
			                                 highlightColor.Frame.Top == contentWrapperView.Frame.Top
			                                );
			//View.ConstrainLayout (() => 
			//                      button.Frame.Width == ButtonWidth &&
			//                      button.Frame.Right == View.Frame.Right - HPadding &&
			//                      button.Frame.Top == View.Frame.Top + VPadding &&

			//                      text.Frame.Left == View.Frame.Left + HPadding &&
			//                      text.Frame.Right == button.Frame.Left - HPadding &&
			//                      text.Frame.Top == button.Frame.Top
			//                     );
			//const int offsetStart = 10;
			//Accessory = UITableViewCellAccessory.DisclosureIndicator;
			//jobId = new UILabel(new CGRect(offsetStart, 0, 75, 40));
			//hours = new UILabel(new CGRect(UIScreen.MainScreen.Bounds.Right - 85, 0, 55, 40));
			//hours.TextAlignment = UITextAlignment.Right;
			//jobName = new UILabel(new CGRect(jobId.Frame.Right, 0, UIScreen.MainScreen.Bounds.Width - jobId.Frame.Width - hours.Frame.Width - (3 * offsetStart), 40));
			//jobName.AdjustsFontSizeToFitWidth = true;
			//jobName.Lines = 0;
			//jobName.Font = jobName.Font.WithSize(10);
			//ContentView.AddSubviews(jobId, jobName, hours);
		}

		private void InitializeBindings()
		{
			
			//this.DelayBind(() =>
			//{
			//	var set = this.CreateBindingSet<HoursEntryCell, EnterTime>();
			//	set.Bind(jobId).To(vm => vm.JobId);
			//	set.Bind(hours).To(vm => vm.Hours);
			//	set.Bind(jobName).To(vm => vm.JobName);
			//	set.Apply();
			//});
		}
	}

}

