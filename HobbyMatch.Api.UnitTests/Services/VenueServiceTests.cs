using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
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
        _venueRepositoryMock.Setup(repo => repo.GetVenueByIdAsync(1))
            .ReturnsAsync(venue);

        var result = await _venueService.GetVenueByIdAsync(1);

        Assert.Equal(venue, result);
    }

    [Fact]
    public async Task GetClientVenuesAsync_ReturnsPaginatedVenues()
    {
        var paginatedData = new PaginatedData<Venue>([new Venue { Id = 1, Name = "Venue A" }],
            1, 1, 1);
        var paginationParams = new PaginationParameters(1, 10);

        _venueRepositoryMock.Setup(repo => repo.GetBusinessClientVenuesAsync(42, paginationParams))
            .ReturnsAsync(paginatedData);

        var result = await _venueService.GetClientVenuesAsync(42, paginationParams);

        Assert.Single(result.Data);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task GetFilteredVenuesAsync_ReturnsFilteredVenues()
    {
        var paginatedData = new PaginatedData<Venue>([new Venue { Id = 2, Name = "Filtered Venue" }],
            1, 1, 1);
        var paginationParams = new PaginationParameters(1, 10);

        _venueRepositoryMock.Setup(repo => repo.GetFilteredVenuesAsync("Filtered", paginationParams))
            .ReturnsAsync(paginatedData);

        var result = await _venueService.GetFilteredVenuesAsync("Filtered", paginationParams);

        Assert.Single(result.Data);
        Assert.Equal("Filtered Venue", result.Data[0].Name);
    }

    [Fact]
    public async Task CreateVenue_CreatesAndReturnsVenue()
    {
        var createRequest = new CreateVenueDto("New Venue", "Here",
            10, 10.0m, new Location { Longitude = 1, Latitude = 1 }, "Nice place");
        Venue? capturedVenue = null;
        _venueRepositoryMock.Setup(repo => repo.AddVenueAsync(It.IsAny<Venue>()))
            .Callback<Venue>(v => capturedVenue = v)
            .Returns(Task.CompletedTask);

        var result = await _venueService.CreateVenue(createRequest, 5);

        Assert.NotNull(capturedVenue);
        Assert.Equal(createRequest.Name, capturedVenue!.Name);
        Assert.Equal(5, capturedVenue.BusinessClientId);
        Assert.Equal(capturedVenue, result);
    }
}