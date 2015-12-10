
namespace TekConf.Mobile.Core.ViewModel
{
	public interface ISettingsService
	{
		string UserIdToken { get; set; }
		string Auth0Domain { get;  }
		string Auth0ClientId { get;  }
	}

}