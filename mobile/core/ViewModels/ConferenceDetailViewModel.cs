using System;
using MvvmCross.Core.ViewModels;

namespace TekConf.Mobile.Core.ViewModels
{
    public class ConferenceDetailViewModel : MvxViewModel
    {
        public void Init(NavObject navObject)
        {
            this.Slug = navObject.Slug;
        }

        private string _slug;
        public string Slug
        {
            get
            {
                return _slug;
            }
            set
            {
                SetProperty(ref _slug, value);
            }
        }

        public class NavObject
        {
            public string Slug { get; set; }
        }
    }
}