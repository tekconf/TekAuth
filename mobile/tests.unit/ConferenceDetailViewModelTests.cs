using Moq;
using Ploeh.AutoFixture;
using Should;
using Tekconf.DTO;
using TekConf.Mobile.Core.Services;
using TekConf.Mobile.Core.ViewModels;
using Xunit;

namespace TekConf.Tests.Unit.Mobile.Core
{
    public class ConferenceDetailViewModelTests
    {
        private readonly IFixture _fixture;
        public ConferenceDetailViewModelTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Conference_should_be_set_on_ctor()
        {
            var schedulesService = new Mock<ISchedulesService>();
            var settingsService = new Mock<ISettingsService>();

            var conference = _fixture.Create<Conference>();

            var vm = new ConferenceDetailViewModel2(conference, schedulesService.Object, settingsService.Object);

            vm.Conference.ShouldEqual(conference);
        }

        [Fact]
        public void Can_add_conference_to_schedule_with_a_user_token()
        {
            var userIdToken = _fixture.Create<string>();
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var schedulesService = new Mock<ISchedulesService>();

            var vm = new ConferenceDetailViewModel2(null, schedulesService.Object, settingsService.Object);

            vm.CanAddToSchedule().ShouldBeTrue();
        }

        [Fact]
        public void Cannot_add_conference_to_schedule_without_a_user_token()
        {
            var userIdToken = string.Empty;
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var schedulesService = new Mock<ISchedulesService>();

            var vm = new ConferenceDetailViewModel2(null, schedulesService.Object, settingsService.Object);

            vm.CanAddToSchedule().ShouldBeFalse();
        }

        [Fact]
        public void Can_remove_conference_to_schedule_with_a_user_token()
        {
            var userIdToken = _fixture.Create<string>();
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var schedulesService = new Mock<ISchedulesService>();

            var vm = new ConferenceDetailViewModel2(null, schedulesService.Object, settingsService.Object);

            vm.CanRemoveFromSchedule().ShouldBeTrue();
        }

        [Fact]
        public void Cannot_remove_conference_to_schedule_without_a_user_token()
        {
            var userIdToken = string.Empty;
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.UserIdToken).Returns(userIdToken);

            var schedulesService = new Mock<ISchedulesService>();

            var vm = new ConferenceDetailViewModel2(null, schedulesService.Object, settingsService.Object);

            vm.CanRemoveFromSchedule().ShouldBeFalse();
        }


    }
}