using System.Linq;
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
        private readonly IFixture _fixture;
        public ConferencesViewModelTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Conferences_should_be_empty_to_start()
        {
            var settingsService = new Mock<ISettingsService>();
            var conferencesService = new Mock<IConferencesService>();

            var conferences = _fixture.CreateMany<Conference>().ToList();
            conferencesService.Setup(x => x.GetConferences(It.IsAny<string>(), It.IsAny<Priority>()))
                .ReturnsAsync(conferences);

            var vm = new ConferencesListViewModel(settingsService.Object, conferencesService.Object);

            vm.Conferences.ShouldBeEmpty();

        }

        [Fact]
        public void Can_load_conferences_with_a_user_token()
        {
            var userIdToken = _fixture.Create<string>();
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var conferencesService = new Mock<IConferencesService>();
            
            var vm = new ConferencesListViewModel(settingsService.Object, conferencesService.Object);

            vm.CanLoadConferences().ShouldBeTrue();
        }

        [Fact]
        public void Cannot_load_conferences_without_a_user_token()
        {
            var userIdToken = string.Empty;
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var conferencesService = new Mock<IConferencesService>();

            var vm = new ConferencesListViewModel(settingsService.Object, conferencesService.Object);

            vm.CanLoadConferences().ShouldBeFalse();
        }

        [Fact]
        public void LoadConferences_should_load_conferences()
        {
            
            var userIdToken = _fixture.Create<string>();
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var conferences = _fixture.CreateMany<Conference>().ToList();
            var conferencesService = new Mock<IConferencesService>();
            conferencesService.Setup(x => x.GetConferences(userIdToken, It.IsAny<Priority>())).ReturnsAsync(conferences);


            var vm = new ConferencesListViewModel(settingsService.Object, conferencesService.Object);

            vm.LoadConferencesCommand.Execute(Priority.Explicit);

            vm.Conferences.ShouldNotBeEmpty();
            conferencesService.Verify(x => x.GetConferences(userIdToken, It.IsAny<Priority>()), Times.Once);

        }

    }
}