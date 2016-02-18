using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Tekconf.DTO;
using TekConf.Mobile.Core.ViewModels;
using UIKit;

namespace ios.Views
{
    public partial class ConferenceDetailView : MvxViewController
    {
        public ConferenceDetailView() : base("ConferenceDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<ConferenceDetailView, ConferenceDetailViewModel>();
            set.Bind(slug).To(vm => vm.Slug);
            set.Apply();
        }
    }
}