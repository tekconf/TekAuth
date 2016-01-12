using Refit;
using System;
using System.Net.Http;
using Fusillade;

namespace TekConf.Mobile.Core.Services
{
	public class ApiService : IApiService
	{
		public const string ApiBaseAddress = "https://tekauth.azurewebsites.net/api";

		private readonly ISettingsService _settingsService;

		public ApiService(ISettingsService settingsService)
		{
			_settingsService = settingsService;
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