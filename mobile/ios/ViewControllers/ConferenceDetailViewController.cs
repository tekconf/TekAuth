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

		private Binding<string, string> _nicknameLabelBinding;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_nicknameLabelBinding = this.SetBinding (
				() => Vm.Name,
				() => conferenceName.Text);
//
//			_emailLabelBinding = this.SetBinding (
//				() => Vm.Email,
//				() => email.Text);
		}
	}
}
