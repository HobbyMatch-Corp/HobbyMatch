using HobbyMatch.BL.Services.Events;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.DbIntegrationTests
{
    public class EventTests : BaseIntegrationTest
    {

        private readonly IEventRepository _eventRepository;
        private readonly IEventService _eventService;

        public EventTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _eventRepository = new EventRepository(DbContext);
            _eventService = new EventService(_eventRepository, null);
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

        [Fact]
        public async Task CreateEvent_ValidData_ShouldPass()
        {
            // Arrange
            CreateEventRequest createEventRequest = new(
                "Test Event",
                "A sample description",
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                new LocationNullable(),
                20.0f,
                100,
                10
            );
            var organizerId = 1;
            var expectedEvent = new Event
            {
                Name = createEventRequest.Name,
                Description = createEventRequest.Description,
                StartTime = createEventRequest.StartTime,
                EndTime = createEventRequest.EndTime,
                Location = createEventRequest.Location,
                Price = createEventRequest.Price,
                MaxUsers = createEventRequest.MaxUsers,
                MinUsers = createEventRequest.MinUsers,
                OrganizerId = organizerId
            };

            // Act
            var result = await _eventService.CreateEventAsync(createEventRequest, organizerId);

            // Assert
            //  Compare method result
            Assert.NotNull(result);
            Assert.Equal(expectedEvent.Name, result.Name);
            Assert.Equal(expectedEvent.Description, result.Description);
            Assert.Equal(expectedEvent.StartTime, result.StartTime);
            Assert.Equal(expectedEvent.EndTime, result.EndTime);
            Assert.Equal(expectedEvent.Price, result.Price);
            Assert.Equal(expectedEvent.MaxUsers, result.MaxUsers);
            Assert.Equal(expectedEvent.MinUsers, result.MinUsers);
            Assert.Equal(expectedEvent.OrganizerId, result.OrganizerId);

            //  Compare what is actually saved in database
            var ev = await DbContext.Events
                .FirstOrDefaultAsync(e => e.Id == result.Id);
            Assert.NotNull(ev);
            Assert.Equal(ev.Name, result.Name);
            Assert.Equal(ev.Description, result.Description);
            Assert.Equal(ev.StartTime, result.StartTime);
            Assert.Equal(ev.EndTime, result.EndTime);
            Assert.Equal(ev.Price, result.Price);
            Assert.Equal(ev.MaxUsers, result.MaxUsers);
            Assert.Equal(ev.MinUsers, result.MinUsers);
            Assert.Equal(ev.OrganizerId, result.OrganizerId);
        }
    }
}
