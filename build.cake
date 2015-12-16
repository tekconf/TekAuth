/*
match
cert
produce
ios-beta
	core-build
		restore-packages
		msbuild
	core-tests
		unit tests
	ios-appstore-build
		ios-build - DotNetBuild
		ios-sign - DotNetBuild
		ios-publish - DotNetBuild
	ui-tests (depends on debug) - uitest
		run uitest
	publish to test cloud (depends on debug) - testcloud
	fastlane.snapshot
	fastlane.frameit
	fastlane.sigh
	fastlane.gym
	fastlane.pilot
*/

#addin "Cake.Xamarin"

using System.Xml;

var target = Argument("target", "Default");

var libs = new Dictionary<string, string> {
	{ "./TekAuth.iOS.sln", "Any" }
};

var buildAction = new Action<Dictionary<string, string>> (solutions => {

	foreach (var sln in solutions) {

		if ((sln.Value == "Any")
				|| (sln.Value == "Win" && IsRunningOnWindows ())
				|| (sln.Value == "Mac" && IsRunningOnUnix ())) {
			
			if (IsRunningOnWindows ()) {
				NuGetRestore (sln.Key, new NuGetRestoreSettings {
					ToolPath = "./tools/nuget3.exe"
				});

				MSBuild (sln.Key, c => { 
					c.Configuration = "Release";
					c.MSBuildPlatform = MSBuildPlatform.x86;
				});
			} else {
				NuGetRestore (sln.Key);

				DotNetBuild (sln.Key, c => c.Configuration = "Release");
			}
		}
	}
});

Task("Default").Does(() =>
{
  Information("Default");
});

Task("ios-beta")
	.Does(() =>
{
  Information("iOS beta");

	var xml = XDocument.Load("./mobile/iOS/info.plist");
	/*
  System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
	
	doc.Load("./mobile/iOS/info.plist");

	XmlNode root = doc.DocumentElement;

	XmlNode node = root.SelectSingleNode("//dict/key[text()=\"CFBundleShortVersionString\"]/following-sibling::string[1]");
  	
  	Console.WriteLine(node.Value);
  	*/

});

/*
Task("core-build").Does(() =>
{
  Information("Core Build");
  buildAction (libs);
});

Task("core-tests")
	.IsDependentOn ("core-build")
	.Does(() =>
{
  Information("Core Tests");
});

Task("ios-appstore-build")
	.IsDependentOn ("core-tests")
	.IsDependentOn ("core-build")	
	.Does(() =>
{
  Information("iOS AppStore Build");
});

Task("ios-ui-tests")
	.IsDependentOn ("ios-appstore-build")
	.Does(() =>
{
  Information("iOS UITests");
});

Task("publish-to-test-cloud")
	.IsDependentOn ("ios-appstore-build")
	.Does(() =>
{
  Information("Publish to TestCloud");
});

Task("snapshot").Does(() =>
{
  Information("snapshot");
});

Task("frameit").Does(() =>
{
  Information("frameit");
});

Task("sigh").Does(() =>
{
  Information("sigh");
});

Task("pilot").Does(() =>
{
  Information("pilot");
});
*/




RunTarget(target);