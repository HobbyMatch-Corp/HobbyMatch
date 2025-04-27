using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Moq;
using HobbyMatch.BL.Services.Events;
using HobbyMatch.Database.Repositories.Events;

namespace UnitTests
{
    public class EventServiceTests
    {
        private IEventService _eventService;
        private Mock<IEventRepository> _eventRepository;

        public EventServiceTests()
        {
            _eventRepository = new Mock<IEventRepository>();
            _eventService = new EventService(_eventRepository.Object);
        }

        [Fact]
        public async Task CreateEventAsync_ReturnsCreatedEvent_OnCorrectValuesPassed()
        {
            // Arrange
            var createEventRequest = new CreateEventRequest(
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
            _eventRepository
                .Setup(x => x.AddEvent(It.IsAny<Event>()))
                .ReturnsAsync(expectedEvent); // Mocking AddEvent to return what we expect
                                              // Act
            var result = await _eventService.CreateEventAsync(createEventRequest, organizerId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEvent.Name, result.Name);
            Assert.Equal(expectedEvent.Description, result.Description);
            Assert.Equal(expectedEvent.StartTime, result.StartTime);
            Assert.Equal(expectedEvent.EndTime, result.EndTime);
            Assert.Equal(expectedEvent.Price, result.Price);
            Assert.Equal(expectedEvent.MaxUsers, result.MaxUsers);
            Assert.Equal(expectedEvent.MinUsers, result.MinUsers);
            Assert.Equal(expectedEvent.OrganizerId, result.OrganizerId);
            _eventRepository.Verify(x => x.AddEvent(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task EditEventAsync_ReturnsUpdatedEvent_WhenEventExistsAndOrganizerMatches()
        {
            // Arrange
            var eventId = 1;
            var organizerId = 123;
            var existingEvent = new Event
            {
                Id = eventId,
                Name = "Old Name",
                Description = "Old Description",
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(2),
                Location = new LocationNullable(),
                Price = 50,
                OrganizerId = organizerId
            };
            var updateRequest = new CreateEventRequest(
                "Updated Name",
                "Updated Description",
                DateTime.UtcNow.AddDays(1),
                DateTime.UtcNow.AddDays(1).AddHours(2),
                new LocationNullable(),
                75.0f,
                100,
                10
            );
            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(existingEvent);
            _eventRepository.Setup(x => x.UpdateEventAsync(It.IsAny<Event>()))
                            .Returns(Task.CompletedTask);
            // Act
            var result = await _eventService.EditEventAsync(updateRequest, eventId, organizerId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateRequest.Name, result.Name);
            Assert.Equal(updateRequest.Description, result.Description);
            Assert.Equal(updateRequest.StartTime, result.StartTime);
            Assert.Equal(updateRequest.EndTime, result.EndTime);
            Assert.Equal(updateRequest.Price, result.Price);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.UpdateEventAsync(It.IsAny<Event>()), Times.Once);
        }

        #region AddUserToEventAsync Tests

        [Fact]
        public async Task AddUserToEventAsync_ReturnsTrue_WhenUserAddedSuccessfully()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = new List<User>(),
                MaxUsers = 5
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);
            _eventRepository.Setup(x => x.SaveChangesAsync())
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _eventService.AddUserToEventAsync(eventId, user);

            // Assert
            Assert.True(result);
            Assert.Contains(user, eventEntity.SignUpList);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddUserToEventAsync_ReturnsFalse_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync((Event)null);

            // Act
            var result = await _eventService.AddUserToEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddUserToEventAsync_ReturnsFalse_WhenSignUpListIsNull()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = null,
                MaxUsers = 5
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.AddUserToEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddUserToEventAsync_ReturnsFalse_WhenUserAlreadySignedUp()
        {
            // Arrange
            var eventId = 1;
            var userId = 10;
            var user = new User { Id = userId };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = new List<User> { new User { Id = userId } },
                MaxUsers = 5
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.AddUserToEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            Assert.Single(eventEntity.SignUpList);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddUserToEventAsync_ReturnsFalse_WhenEventIsFull()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = new List<User>
                {
                    new User { Id = 1 },
                    new User { Id = 2 }
                },
                MaxUsers = 2
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.AddUserToEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            Assert.Equal(2, eventEntity.SignUpList.Count);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        #endregion

        #region RemoveUserFromEventAsync Tests

        [Fact]
        public async Task RemoveUserFromEventAsync_ReturnsTrue_WhenUserRemovedSuccessfully()
        {
            // Arrange
            var eventId = 1;
            var userId = 10;
            var user = new User { Id = userId };
            var existingUser = new User { Id = userId };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = new List<User> { existingUser }
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);
            _eventRepository.Setup(x => x.SaveChangesAsync())
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _eventService.RemoveUserFromEventAsync(eventId, user);

            // Assert
            Assert.True(result);
            Assert.Empty(eventEntity.SignUpList);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveUserFromEventAsync_ReturnsFalse_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync((Event)null);

            // Act
            var result = await _eventService.RemoveUserFromEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RemoveUserFromEventAsync_ReturnsFalse_WhenSignUpListIsNull()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = null
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.RemoveUserFromEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RemoveUserFromEventAsync_ReturnsFalse_WhenUserNotFound()
        {
            // Arrange
            var eventId = 1;
            var user = new User { Id = 10 };
            var eventEntity = new Event
            {
                Id = eventId,
                SignUpList = new List<User> { new User { Id = 5 } }
            };

            _eventRepository.Setup(x => x.GetEventByIdAsync(eventId))
                            .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.RemoveUserFromEventAsync(eventId, user);

            // Assert
            Assert.False(result);
            Assert.Single(eventEntity.SignUpList);
            _eventRepository.Verify(x => x.GetEventByIdAsync(eventId), Times.Once);
            _eventRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        #endregion

        #region GetSponsoredEventsAsync Tests

        [Fact]
        public async Task GetSponsoredEventsAsync_ReturnsEvents_WhenEventsExist()
        {
            // Arrange
            var businessEmail = "business@example.com";
            var expectedEvents = new List<Event>
            {
                new Event { Id = 1, Name = "Sponsored Event 1" },
                new Event { Id = 2, Name = "Sponsored Event 2" }
            };

            _eventRepository.Setup(x => x.GetSponsoredEventsAsync(businessEmail))
                            .ReturnsAsync(expectedEvents);

            // Act
            var result = await _eventService.GetSponsoredEventsAsync(businessEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(expectedEvents, result);
            _eventRepository.Verify(x => x.GetSponsoredEventsAsync(businessEmail), Times.Once);
        }

        [Fact]
        public async Task GetSponsoredEventsAsync_ReturnsEmptyList_WhenNoEventsExist()
        {
            // Arrange
            var businessEmail = "business@example.com";
            var emptyList = new List<Event>();

            _eventRepository.Setup(x => x.GetSponsoredEventsAsync(businessEmail))
                            .ReturnsAsync(emptyList);

            // Act
            var result = await _eventService.GetSponsoredEventsAsync(businessEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _eventRepository.Verify(x => x.GetSponsoredEventsAsync(businessEmail), Times.Once);
        }

        [Fact]
        public async Task GetSponsoredEventsAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var businessEmail = "business@example.com";

            _eventRepository.Setup(x => x.GetSponsoredEventsAsync(businessEmail))
                            .ReturnsAsync((List<Event>)null);

            // Act
            var result = await _eventService.GetSponsoredEventsAsync(businessEmail);

            // Assert
            Assert.Null(result);
            _eventRepository.Verify(x => x.GetSponsoredEventsAsync(businessEmail), Times.Once);
        }

        #endregion
    }
}