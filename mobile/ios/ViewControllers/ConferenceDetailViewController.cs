using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace ios
{
	partial class ConferenceDetailViewController : UIViewController
	{
		public ConferenceDetailViewController (IntPtr handle) : base (handle)
		{
		}

		private ConferenceDetailViewModel Vm {
			get {
				return Application.Locator.Conference;
			}
		}

		private Binding<string, string> _nameBinding;
		private Binding<string, string> _descriptionBinding;
		private Binding<DateTime?, string> _startDateBinding;
		private Binding<DateTime?, string> _endDateBinding;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_nameBinding = this.SetBinding (
				() => Vm.Name,
				() => conferenceName.Text);

			_descriptionBinding = this.SetBinding (
				() => Vm.Description,
				() => conferenceDescription.Text);

			_startDateBinding = this.SetBinding (
				() => Vm.StartDate,
				() => conferenceStartDate.Text);

			_endDateBinding = this.SetBinding (
				() => Vm.EndDate,
				() => conferenceEndDate.Text);

//			_emailLabelBinding = this.SetBinding (
//				() => Vm.Email,
//				() => email.Text);
		}
	}
}
