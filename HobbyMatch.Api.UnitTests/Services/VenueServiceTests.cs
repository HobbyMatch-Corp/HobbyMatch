using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.Domain.Entities;
using Moq;
using System;

namespace UnitTests;

public class VenueServiceTests
{
    private readonly Mock<IVenueRepository> _venueRepositoryMock;
    private readonly IVenueService _venueService;

    public VenueServiceTests()
    {
        _venueRepositoryMock = new Mock<IVenueRepository>();
        _venueService = new VenueService(_venueRepositoryMock.Object);
    }

    [Fact]
    public async Task GetVenueByIdAsync_ReturnsVenue_WhenExists()
    {
        var venue = new Venue { Id = 1, Name = "Test Venue" };
        _venueRepositoryMock.Setup(repo => repo.GetVenueByIdAsync(1)).ReturnsAsync(venue);

        var result = await _venueService.GetVenueByIdAsync(1);

        Assert.Equal(venue, result);
    }

    [Fact]
    public async Task GetClientVenuesAsync_ReturnsPaginatedVenues()
    {
        var data = new List<Venue>
        {
            new() { Id = 1, Name = "Venue A" },
        };
        var paginationParams = new PaginationParameters(1, 10);

        _venueRepositoryMock
            .Setup(repo => repo.GetBusinessClientVenuesAsync(42))
            .ReturnsAsync(data);

        var result = await _venueService.GetClientVenuesAsync(42);

        Assert.Single(result);
    }

    [Fact]
    public async Task GetFilteredVenuesAsync_ReturnsFilteredVenues()
    {
        var data = new List<Venue>
        {
            new() { Id = 2, Name = "Filtered Venue" },
        };

        _venueRepositoryMock
            .Setup(repo => repo.GetFilteredVenuesAsync("Filtered"))
            .ReturnsAsync(data);

        var result = await _venueService.GetFilteredVenuesAsync("Filtered");

        Assert.Single(result);
        Assert.Equal("Filtered Venue", result[0].Name);
    }

    [Fact]
    public async Task CreateVenue_CreatesAndReturnsVenue()
    {
        var createRequest = new CreateVenueDto(
            "New Venue",
            "Nice place",
            "Here",
            new Location { Longitude = 1, Latitude = 1 }
        );
        Venue? capturedVenue = null;
        _venueRepositoryMock
            .Setup(repo => repo.AddVenueAsync(It.IsAny<Venue>()))
            .Callback<Venue>(v => capturedVenue = v)
            .Returns(Task.CompletedTask);

        var result = await _venueService.CreateVenue(createRequest, 5);

        Assert.NotNull(capturedVenue);
        Assert.Equal(createRequest.Name, capturedVenue!.Name);
        Assert.Equal(5, capturedVenue.BusinessClientId);
        Assert.Equal(capturedVenue, result);
    }

    [Fact]

    public async Task GetVenuesAsync_ReturnsCorrectVenueList_WhenVenueListExists()
    {
        // Arrange
        var data = new List<Venue>
        {
            new() { Id = 2, Name = "Venue" },
            new() { Id = 3, Name = "Venue3" },
        };

        _venueRepositoryMock
            .Setup(repo => repo.GetVenuesAsync())
            .ReturnsAsync(data);

        // Act
        var result = await _venueService.GetVenuesAsync();

        // Assert
        Assert.Equal(data, result);
        Assert.Equal(data.Count(), result.Count());
    }

    [Fact]
    public async Task GetVenuesAsync_ReturnsEmptyList_WhenListIsEmpty()
    {
        // Arrange
        var data = new List<Venue>();

        _venueRepositoryMock
            .Setup(repo => repo.GetVenuesAsync())
            .ReturnsAsync(data);

        // Act
        var result = await _venueService.GetVenuesAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task EditVenueAsync_ReturnsFalse_WhenIdIsInvalid()
    {
        // Arrange
        var updateVenueDto = new UpdateVenueDto("name", "description");
        _venueRepositoryMock.Setup(repo => repo.GetVenueByIdAsync(3)).ReturnsAsync((Venue?)null);

        // Act

        var result = await _venueService.EditVenueAsync(3, updateVenueDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task EditVenueAsync_VenueExists_UpdatesVenueAndSaves_ReturnsTrue()
    {
        // Arrange
        var venueId = 1;
        var updateDto = new UpdateVenueDto("Updated Name", "Updated Description");
        var existingVenue = new Venue{Id = venueId, Name = "Old Name", Description = "Old Description" };
		var mockRepo = new Mock<IVenueRepository>();

		mockRepo.Setup(repo => repo.GetVenueByIdAsync(venueId)).ReturnsAsync(existingVenue);
		mockRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

		var service = new VenueService(mockRepo.Object);

		// Act
		var result = await service.EditVenueAsync(venueId, updateDto);

		// Assert
		Assert.True(result);
		Assert.Equal(updateDto.Name, existingVenue.Name);
		Assert.Equal(updateDto.Description, existingVenue.Description);
		mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
	}
}
