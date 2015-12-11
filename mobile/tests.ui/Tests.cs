using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace tests.ui
{
	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void AppLaunches ()
		{
			app.WaitForElement (c => c.Marked ("codemash-2016"), "Timed out", new TimeSpan(0,1,0));
			app.Screenshot ("Conferences loaded");
			app.Tap(c => c.Marked("codemash-2016"));
			app.WaitForElement (c => c.Marked ("Codemash"), "Timed out", new TimeSpan(0,0,10));
			app.Screenshot ("Conference loaded");
			app.Back();
		}
	}
}