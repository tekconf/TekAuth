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
        private MvxImageViewLoader _imageViewLoader;
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
			contentWrapperView = new UIView()
			{
				BackgroundColor = UIColor.White
			};
            contentWrapperView.Layer.BorderColor = UIColor.LightGray.CGColor;
            contentWrapperView.Layer.BorderWidth = 0.5f;


            highlightColor = new UIView { 
				BackgroundColor = UIColor.Green
			};


            image = new UIImageView();
            name = new UILabel()
            {
                Text = "Test test test",
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.FromName("OpenSans-ExtraBold", 16f)
            };

            description = new UILabel()
            {
                Text = "Test test test",
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.FromName("OpenSans-Light", 12f)
            };

            dateView = new UIView
            {
                BackgroundColor = UIColor.FromRGB(127, 127, 127)
            };

            date = new UILabel()
            {
                Text = "Test test test",
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.FromName("OpenSans-Light", 12f),
                TextColor = UIColor.White
            };

            locationView = new UIView
            {
                BackgroundColor = UIColor.FromRGB(230, 230, 230)
            };

            location = new UILabel()
            {
                Text = "Test test test",
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.FromName("OpenSans-Light", 12f),
                TextColor = UIColor.DarkGray
            };

            scheduleStatusView = new UIView
            {
                BackgroundColor = UIColor.LightGray
            };

            scheduleStatus = new UILabel()
            {
                Text = "Test test test",
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.FromName("FontAwesome", 17f)
            };
            
            this.ContentView.Add(contentWrapperView);
            contentWrapperView.Add(highlightColor);
            contentWrapperView.Add(image);
            contentWrapperView.Add(name);
            contentWrapperView.Add(description);
            contentWrapperView.Add(dateView);
            contentWrapperView.Add(date);
            contentWrapperView.Add(locationView);
            contentWrapperView.Add(location);
            contentWrapperView.Add(scheduleStatusView);
            contentWrapperView.Add(scheduleStatus);

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
			                                 highlightColor.Frame.Top == contentWrapperView.Frame.Top &&
                                             
                                             image.Frame.Width == 100f &&
                                             image.Frame.Height == 100f &&
                                             image.Frame.Right == contentWrapperView.Frame.Right - 8f &&
                                             image.Frame.Top == contentWrapperView.Frame.Top + 8f &&

                                             name.Frame.Left == highlightColor.Frame.Right + 4f &&
                                             name.Frame.Right == image.Frame.Left - 4f &&
                                             name.Frame.Height >= 21f &&
                                             name.Frame.Width >= 100f &&
                                             name.Frame.Top == contentWrapperView.Frame.Top + 8f &&

                                             description.Frame.Left == name.Frame.Left &&
                                             description.Frame.Right == name.Frame.Right &&
                                             description.Frame.Top == name.Frame.Bottom + 4f &&
                                             description.Frame.Bottom >= dateView.Frame.Top - 4f &&
                                             description.Frame.Width == name.Frame.Width &&
                                             description.Frame.Height >= 21f &&

                                             scheduleStatusView.Frame.Right == contentWrapperView.Frame.Right &&
                                             scheduleStatusView.Frame.Width == 24f &&
                                             scheduleStatusView.Frame.Height == 24f &&
                                             scheduleStatusView.Frame.Bottom == contentWrapperView.Frame.Bottom &&

                                             dateView.Frame.Height == scheduleStatusView.Frame.Height &&
                                             dateView.Frame.Bottom == contentWrapperView.Frame.Bottom &&
                                             dateView.Frame.Width >= 150f &&
                                             dateView.Frame.Left == highlightColor.Frame.Right &&
                                             dateView.Frame.Right == locationView.Frame.Left &&

                                             date.Frame.Left == dateView.Frame.Left + 4f &&
                                             date.Frame.Right == dateView.Frame.Right - 4f &&
                                             date.Frame.Top == dateView.Frame.Top + 2f &&
                                             date.Frame.Bottom == dateView.Frame.Bottom - 2f &&
                                             date.Frame.Height == 22f &&
                                             date.Frame.Width >= 10f &&

                                             locationView.Frame.Height == scheduleStatusView.Frame.Height &&
                                             locationView.Frame.Bottom == dateView.Frame.Bottom &&
                                             locationView.Frame.Width >= 150f &&
                                             locationView.Frame.Left == dateView.Frame.Right &&
                                             locationView.Frame.Right == scheduleStatusView.Frame.Left &&

                                             location.Frame.Left == locationView.Frame.Left + 4f &&
                                             location.Frame.Right == locationView.Frame.Right - 4f &&
                                             location.Frame.Top == locationView.Frame.Top + 2f &&
                                             location.Frame.Bottom == locationView.Frame.Bottom - 2f



                                            );
		}

		private void InitializeBindings()
		{

            this.DelayBind(() =>
            {
                _imageViewLoader = new MvxImageViewLoader(() => this.image);

                var set = this.CreateBindingSet<ConferenceCell, ConferenceListViewModel>();
                set.Bind(name).To(vm => vm.Name);
                set.Bind(description).To(vm => vm.Description);
                set.Bind(scheduleStatus).To(vm => vm.IsAddedToSchedule).WithConversion("AddedToSchedule");
                set.Bind(date).To(vm => vm.ShortDate);
                set.Bind(location).To(vm => vm.Location);
                set.Bind(highlightColor).For(v => v.BackgroundColor).To(vm => vm.HighlightColor).WithConversion("RGBA");
                set.Bind(scheduleStatusView).For(v => v.BackgroundColor).To(vm => vm.HighlightColor).WithConversion("RGBA");
                set.Bind(_imageViewLoader).To(item => item.ImageUrl);

                set.Apply();
            });
        }
	}

}



