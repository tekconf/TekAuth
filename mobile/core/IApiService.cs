using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tekconf.DTO;

namespace TekConf.Mobile.Core
{
	public interface IApiService
	{
		ITekConfApi Speculative { get; }
		ITekConfApi UserInitiated { get; }
		ITekConfApi Background { get; }
	}
}