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
            var user = new User() { Email = "user1@test.com" };
            var eventRepo = new EventRepository(DbContext);

            // Act
            var res = await eventRepo.GetSignedUpEvents(user);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(3, res.Count);
        }

        [Fact]
        public async Task GetOrganizedEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            var organizer = new User() { Email = "user1@test.com" };
            var eventRepo = new EventRepository(DbContext);

            // Act
            var res = await eventRepo.GetOrganizedEvents(organizer);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async Task GetSponsoredEvents_ValidSeededData_ShouldPass()
        {
            // Arrange
            var bc = new BusinessClient() { Email = "bclient1@test.com" };
            var eventRepo = new EventRepository(DbContext);

            // Act
            var res = await eventRepo.GetSponsoredEvents(bc);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res.Count);
        }
    }
}
