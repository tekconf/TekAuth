using MvvmCross.Core.ViewModels;
using Tekconf.DTO;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TekConf.Mobile.Core.ViewModels
{

    public class FirstViewModel 
        : MvxViewModel
    {
        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }
    }
}
