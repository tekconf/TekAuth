using System.Threading.Tasks;
using Tekconf.DTO;

namespace TekConf.Mobile.Core
{
	public interface IImageService
	{
		Task<string> GetImagePath (Conference conference);
	}
}