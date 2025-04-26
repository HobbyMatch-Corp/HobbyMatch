using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.DbIntegrationTests
{
    public class EventTests : BaseIntegrationTest
    {

        private readonly IEventRepository _eventRepository;

        public EventTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _eventRepository = new EventRepository(DbContext);
        }

        [Fact]
        public async Task GetSigndUpEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            string userEmail = "user2@test.com";

            // Act
            var res = await _eventRepository.GetSignedUpEventsAsync(userEmail);

            // Assert
            Assert.NotNull(res);
            Assert.NotNull(res.Where(e => e.Name == "Sunset Sketch Session").First().Location.Longitude);
            Assert.NotNull(res.Where(e => e.Name == "Sunset Sketch Session").First().Location.Latitude);
            Assert.Equal(4, res.Count);
        }

        [Fact]
        public async Task GetOrganizedEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            string organizerEmail = "user1@test.com";

            // Act
            var res = await _eventRepository.GetOrganizedEventsAsync(organizerEmail);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async Task GetSponsoredEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            var bcEmail = "bclient1@test.com";

            // Act
            var res = await _eventRepository.GetSponsoredEventsAsync(bcEmail);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res.Count);
        }
    }
}
