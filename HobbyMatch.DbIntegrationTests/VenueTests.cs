using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.DbIntegrationTests;

public class VenueTests : BaseIntegrationTest
{
    private readonly IVenueRepository _venueRepository;
    private readonly IVenueService _venueService;

    public VenueTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        _venueRepository = new VenueRepository(DbContext);
        _venueService = new VenueService(_venueRepository);
    }

    [Fact]
    public async Task SeededVenuesExist()
    {
        var filter = "";

        var result = await _venueRepository.GetFilteredVenuesAsync(filter);

        Assert.Contains(result, v => v.Name == "The Creative Canvas");
    }

    [Fact]
    public async Task FilterWorksCorrectly()
    {
        var filter = "Creative";

        var result = await _venueRepository.GetFilteredVenuesAsync(filter);

        Assert.Contains(result, v => v.Name == "The Creative Canvas");
        Assert.Single(result);
    }

    [Fact]
    public async Task GetsBusinessClientVenuesCorrectly()
    {
        var clientId = 1;

        var result = await _venueRepository.GetBusinessClientVenuesAsync(clientId);

        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, v => v.Name == "The Creative Canvas");
    }

    [Fact]
    public async Task GetsVenueDetailsCorrectly()
    {
        var venueId = 1;

        var result = await _venueRepository.GetVenueByIdAsync(venueId);

        Assert.NotNull(result);
        Assert.Equal("The Creative Canvas", result.Name);
        Assert.Equal(25, result.MaxUsers);
    }

    [Fact]
    public async Task AddsVenueCorrectly()
    {
        // Arrange
        var startCount = await DbContext.Venues.CountAsync();
        var businessClientId = 2;

        var request = new CreateVenueDto(
            "Test name",
            "Test description",
            "Test address",
            new Location { Latitude = 1, Longitude = 1 }
        );

        var expectedVenue = new Venue
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            Location = request.Location,
            BusinessClientId = businessClientId
        };

        // Act
        var result = await _venueService.CreateVenue(request, businessClientId);

        // Assert: check result
        Assert.NotNull(result);
        Assert.Equal(expectedVenue.Name, result.Name);
        Assert.Equal(expectedVenue.Address, result.Address);
        Assert.Equal(expectedVenue.Location.Latitude, result.Location.Latitude);
        Assert.Equal(expectedVenue.Location.Longitude, result.Location.Longitude);
        Assert.Equal(expectedVenue.Description, result.Description);
        Assert.Equal(expectedVenue.BusinessClientId, result.BusinessClientId);

        // Assert: check saved entity
        var savedVenue = await DbContext.Venues.FirstOrDefaultAsync(v => v.Id == result.Id);
        Assert.NotNull(savedVenue);
        Assert.Equal(expectedVenue.Name, savedVenue.Name);
        Assert.Equal(expectedVenue.Address, savedVenue.Address);
        Assert.Equal(expectedVenue.MaxUsers, savedVenue.MaxUsers);
        Assert.Equal(expectedVenue.Price, savedVenue.Price);
        Assert.Equal(expectedVenue.Location.Latitude, savedVenue.Location.Latitude);
        Assert.Equal(expectedVenue.Location.Longitude, savedVenue.Location.Longitude);
        Assert.Equal(expectedVenue.Description, savedVenue.Description);
        Assert.Equal(expectedVenue.BusinessClientId, savedVenue.BusinessClientId);

        // Assert: check DB count increment
        var finalCount = await DbContext.Venues.CountAsync();
        Assert.Equal(startCount + 1, finalCount);
    }
}
