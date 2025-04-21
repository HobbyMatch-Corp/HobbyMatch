using HobbyMatch.BL.Services.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Database.Repositories.Users;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Exceptions.AuthExceptions;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Identity;
using Moq;
using MockQueryable;
using HobbyMatch.BL.Services.Event;
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


	}
}
