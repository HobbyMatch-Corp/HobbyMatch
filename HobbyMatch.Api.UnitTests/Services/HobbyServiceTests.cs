using HobbyMatch.BL.Services.Hobbies;
using HobbyMatch.Database.Repositories.Hobbies;
using HobbyMatch.Domain.Entities;
using Moq;

namespace UnitTests;

public class HobbyServiceTests
{
    private readonly Mock<IHobbyRepository> _mockRepository;
    private readonly HobbyService _hobbyService;

    public HobbyServiceTests()
    {
        _mockRepository = new Mock<IHobbyRepository>();
        _hobbyService = new HobbyService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetHobbiesAsync_ReturnsHobbyCollection()
    {
        // Arrange
        var hobbies = new List<Hobby>
        {
            new Hobby { Id = 1, Name = "Reading" },
            new Hobby { Id = 2, Name = "Swimming" }
        };

        _mockRepository.Setup(repo => repo.GetHobbiesAsync())
                       .ReturnsAsync(hobbies);

        // Act
        var result = await _hobbyService.GetHobbiesAsync();

        // Assert
        Assert.Equal(hobbies.Count, result.Count);
        Assert.Contains(result, h => h.Name == "Reading");
        Assert.Contains(result, h => h.Name == "Swimming");
    }

    [Fact]
    public async Task GetHobbyAsync_ById_ReturnsHobby()
    {
        // Arrange
        var hobby = new Hobby { Id = 1, Name = "Running" };

        _mockRepository.Setup(repo => repo.GetHobbyAsync(1))
                       .ReturnsAsync(hobby);

        // Act
        var result = await _hobbyService.GetHobbyAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hobby.Id, result.Id);
        Assert.Equal(hobby.Name, result.Name);
    }

    [Fact]
    public async Task GetHobbyAsync_ByName_ReturnsHobby()
    {
        // Arrange
        var hobby = new Hobby { Id = 2, Name = "Drawing" };

        _mockRepository.Setup(repo => repo.GetHobbyAsync("Drawing"))
                       .ReturnsAsync(hobby);

        // Act
        var result = await _hobbyService.GetHobbyAsync("Drawing");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hobby.Id, result.Id);
        Assert.Equal(hobby.Name, result.Name);
    }

    [Fact]
    public async Task GetHobbyAsync_ById_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetHobbyAsync(99))
                       .ReturnsAsync((Hobby?)null);

        // Act
        var result = await _hobbyService.GetHobbyAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetHobbyAsync_ByName_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetHobbyAsync("Unknown"))
                       .ReturnsAsync((Hobby?)null);

        // Act
        var result = await _hobbyService.GetHobbyAsync("Unknown");

        // Assert
        Assert.Null(result);
    }
}
