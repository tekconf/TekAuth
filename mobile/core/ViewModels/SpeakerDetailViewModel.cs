using GalaSoft.MvvmLight;
using System;
using Tekconf.DTO;

namespace TekConf.Mobile.Core.ViewModels
{
	public class SpeakerDetailViewModel :ViewModelBase
	{
		public Speaker Speaker { get; set; }
		public SpeakerDetailViewModel (Session session, Speaker speaker)
		{
			Speaker = speaker;
		}

	}
	
}