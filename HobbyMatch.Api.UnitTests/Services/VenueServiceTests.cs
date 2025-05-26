using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.Domain.Entities;
using Moq;

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
}
