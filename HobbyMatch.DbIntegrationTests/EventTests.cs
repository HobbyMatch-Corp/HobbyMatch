using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.DbIntegrationTests
{
    public class EventTests : BaseIntegrationTest
    {

        public EventTests(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        [Fact]
        public async Task GetSigndUpEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            string userEmail = "user1@test.com";
            var eventRepo = new EventRepository(DbContext);

            // Act
            var res = await eventRepo.GetSignedUpEventsAsync(userEmail);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(3, res.Count);
        }

        [Fact]
        public async Task GetOrganizedEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            string organizerEmail = "user1@test.com";
            var eventRepo = new EventRepository(DbContext);

            // Act
            var res = await eventRepo.GetOrganizedEventsAsync(organizerEmail);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async Task GetSponsoredEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            var bcEmail = "bclient1@test.com";
            var eventRepo = new EventRepository(DbContext);

            // Act
            var res = await eventRepo.GetSponsoredEventsAsync(bcEmail);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res.Count);
        }
    }
}
