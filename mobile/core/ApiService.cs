using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tekconf.DTO;
using System;
using System.Net.Http;
using Fusillade;
using ModernHttpClient;
using TekConf.Mobile.Core.ViewModel;

namespace TekConf.Mobile.Core
{
	public class ApiService : IApiService
	{
		public const string ApiBaseAddress = "https://tekauth.azurewebsites.net/api";

		private readonly ISettingsService _settingsService;

		public ApiService(ISettingsService settingsService)
		{
			this._settingsService = settingsService;
			Func<HttpMessageHandler, ITekConfApi> createClient = messageHandler =>
			{
				var client = new HttpClient(messageHandler)
				{
					BaseAddress = new Uri(ApiBaseAddress)
				};

				return RestService.For<ITekConfApi>(client);
			};

			_background = new Lazy<ITekConfApi>(() => createClient(
				new RateLimitedHttpMessageHandler(new AuthenticatedHttpClientHandler (_settingsService.UserIdToken), Priority.Background)));

			_userInitiated = new Lazy<ITekConfApi>(() => createClient(
				new RateLimitedHttpMessageHandler(new AuthenticatedHttpClientHandler (_settingsService.UserIdToken), Priority.UserInitiated)));

			_speculative = new Lazy<ITekConfApi>(() => createClient(
				new RateLimitedHttpMessageHandler(new AuthenticatedHttpClientHandler (_settingsService.UserIdToken), Priority.Speculative)));
		}

		private readonly Lazy<ITekConfApi> _background;
		private readonly Lazy<ITekConfApi> _userInitiated;
		private readonly Lazy<ITekConfApi> _speculative;

		public ITekConfApi Background
		{
			get { return _background.Value; }
		}

		public ITekConfApi UserInitiated
		{
			get { return _userInitiated.Value; }
		}

		public ITekConfApi Speculative
		{
			get { return _speculative.Value; }
		}
	}
}