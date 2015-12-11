using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tekconf.DTO;

namespace TekConf.Mobile.Core
{
	[Headers("Accept: application/json")]
	public interface ITekConfApi
	{
		[Get("/conferences")]
		[Headers("Authorization: Bearer")]
		Task<List<Conference>> GetConferences();

		[Get("/conferences/{slug}")]
		Task<Conference> GetConference(string slug);
	}
}