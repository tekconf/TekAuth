using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using Refit;
using System.Net.Http;
using System;
using System.Collections.ObjectModel;
using Tekconf.DTO;

namespace TekConf.Mobile.Core.ViewModel
{
	public interface ISettingsService
	{
		string UserIdToken { get; set; }
	}

}