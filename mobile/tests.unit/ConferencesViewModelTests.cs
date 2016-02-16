using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Fusillade;
using Moq;
using Ploeh.AutoFixture;
using Should;
using Tekconf.DTO;
using TekConf.Mobile.Core.Services;
using TekConf.Mobile.Core.ViewModels;
using Xunit;

namespace TekConf.Tests.Unit.Mobile.Core
{
	public class ConferencesViewModelTests
	{
	    public ConferencesViewModelTests()
	    {
	        
	    }

		[Fact]
		public async Task LoadConferences_should_load_conferences ()
		{
		    var settingsService = new Mock<ISettingsService>();
		    var conferencesService = new Mock<IConferencesService>();
            var fixture = new Fixture();

		    var conferences = fixture.CreateMany<Conference>().ToList();
		    conferencesService.Setup(x => x.GetConferences(It.IsAny<string>(), It.IsAny<Priority>())).ReturnsAsync(conferences);

            var vm = new ConferencesViewModel(settingsService.Object, conferencesService.Object);

            vm.Conferences.ShouldBeEmpty();
		    await vm.LoadConferences(Priority.Explicit);
            vm.Conferences.ShouldNotBeEmpty();
		}
        
	}
}