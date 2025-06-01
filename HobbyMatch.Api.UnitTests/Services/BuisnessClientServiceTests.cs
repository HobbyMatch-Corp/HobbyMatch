using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.BL.Services.BusinessClients;
using HobbyMatch.Database.Repositories.BusinessClients;
using HobbyMatch.Domain.Entities;
using Moq;

namespace UnitTests
{
    public class BusinessClientServiceTests
    {
        private readonly IBusinessClientService _businessClientService;
        private readonly Mock<IBusinessClientRepository> _businessClientRepository;

        public BusinessClientServiceTests()
        {
            _businessClientRepository = new Mock<IBusinessClientRepository>();
            _businessClientService = new BusinessClientService(_businessClientRepository.Object);
        }

        #region GetBusinessClientByIdAsync Tests

        [Fact]
        public async Task GetBusinessClientByIdAsync_ReturnsBusinessClient_WhenBusinessClientExists()
        {
            // Arrange
            var businessClientId = 1;
            var expectedBusinessClient = new BusinessClient
            {
                Id = businessClientId,
                UserName = "TestClient",
                Email = "test@example.com",
                TaxID = "123456789"
            };

            _businessClientRepository.Setup(x => x.GetBusinessClientByIdAsync(businessClientId))
                                   .ReturnsAsync(expectedBusinessClient);

            // Act
            var result = await _businessClientService.GetBusinessClientByIdAsync(businessClientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBusinessClient.Id, result.Id);
            Assert.Equal(expectedBusinessClient.UserName, result.UserName);
            Assert.Equal(expectedBusinessClient.Email, result.Email);
            Assert.Equal(expectedBusinessClient.TaxID, result.TaxID);
            _businessClientRepository.Verify(x => x.GetBusinessClientByIdAsync(businessClientId), Times.Once);
        }

        [Fact]
        public async Task GetBusinessClientByIdAsync_ReturnsNull_WhenBusinessClientDoesNotExist()
        {
            // Arrange
            var businessClientId = 999;

            _businessClientRepository.Setup(x => x.GetBusinessClientByIdAsync(businessClientId))
                                   .ReturnsAsync((BusinessClient?)null);

            // Act
            var result = await _businessClientService.GetBusinessClientByIdAsync(businessClientId);

            // Assert
            Assert.Null(result);
            _businessClientRepository.Verify(x => x.GetBusinessClientByIdAsync(businessClientId), Times.Once);
        }

        #endregion

        #region GetBusinessClientByEmailAsync Tests

        [Fact]
        public async Task GetBusinessClientByEmailAsync_ReturnsBusinessClient_WhenBusinessClientExists()
        {
            // Arrange
            var email = "test@example.com";
            var expectedBusinessClient = new BusinessClient
            {
                Id = 1,
                UserName = "TestClient",
                Email = email,
                TaxID = "123456789"
            };

            _businessClientRepository.Setup(x => x.GetBusinessClientByEmailAsync(email))
                                   .ReturnsAsync(expectedBusinessClient);

            // Act
            var result = await _businessClientService.GetBusinessClientByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBusinessClient.Id, result.Id);
            Assert.Equal(expectedBusinessClient.UserName, result.UserName);
            Assert.Equal(expectedBusinessClient.Email, result.Email);
            Assert.Equal(expectedBusinessClient.TaxID, result.TaxID);
            _businessClientRepository.Verify(x => x.GetBusinessClientByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task GetBusinessClientByEmailAsync_ReturnsNull_WhenBusinessClientDoesNotExist()
        {
            // Arrange
            var email = "nonexistent@example.com";

            _businessClientRepository.Setup(x => x.GetBusinessClientByEmailAsync(email))
                                   .ReturnsAsync((BusinessClient?)null);

            // Act
            var result = await _businessClientService.GetBusinessClientByEmailAsync(email);

            // Assert
            Assert.Null(result);
            _businessClientRepository.Verify(x => x.GetBusinessClientByEmailAsync(email), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetBusinessClientByEmailAsync_ReturnsNull_WhenEmailIsEmptyOrWhitespace(string email)
        {
            // Arrange
            _businessClientRepository.Setup(x => x.GetBusinessClientByEmailAsync(email))
                                   .ReturnsAsync((BusinessClient?)null);

            // Act
            var result = await _businessClientService.GetBusinessClientByEmailAsync(email);

            // Assert
            Assert.Null(result);
            _businessClientRepository.Verify(x => x.GetBusinessClientByEmailAsync(email), Times.Once);
        }

        #endregion

        #region GetBusinessClientsAsync Tests

        [Fact]
        public async Task GetBusinessClientsAsync_ReturnsListOfBusinessClients_WhenBusinessClientsExist()
        {
            // Arrange
            var expectedBusinessClients = new List<BusinessClient>
            {
                new BusinessClient { Id = 1, UserName = "Client1", Email = "client1@example.com", TaxID = "111111111" },
                new BusinessClient { Id = 2, UserName = "Client2", Email = "client2@example.com", TaxID = "222222222" },
                new BusinessClient { Id = 3, UserName = "Client3", Email = "client3@example.com", TaxID = "333333333" }
            };

            _businessClientRepository.Setup(x => x.GetBusinessClientsAsync())
                                   .ReturnsAsync(expectedBusinessClients);

            // Act
            var result = await _businessClientService.GetBusinessClientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBusinessClients.Count, result.Count);
            for (int i = 0; i < expectedBusinessClients.Count; i++)
            {
                Assert.Equal(expectedBusinessClients[i].Id, result[i].Id);
                Assert.Equal(expectedBusinessClients[i].UserName, result[i].UserName);
                Assert.Equal(expectedBusinessClients[i].Email, result[i].Email);
                Assert.Equal(expectedBusinessClients[i].TaxID, result[i].TaxID);
            }
            _businessClientRepository.Verify(x => x.GetBusinessClientsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetBusinessClientsAsync_ReturnsEmptyList_WhenNoBusinessClientsExist()
        {
            // Arrange
            var expectedBusinessClients = new List<BusinessClient>();

            _businessClientRepository.Setup(x => x.GetBusinessClientsAsync())
                                   .ReturnsAsync(expectedBusinessClients);

            // Act
            var result = await _businessClientService.GetBusinessClientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _businessClientRepository.Verify(x => x.GetBusinessClientsAsync(), Times.Once);
        }

        #endregion

        #region UpdateBusinessClientAsync Tests

        [Fact]
        public async Task UpdateBusinessClientAsync_ReturnsUpdatedBusinessClient_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = 1;
            var updateDto = new UpdateBusinessClientDto("UpdatedClient", "updated@example.com", "987654321");

            var expectedUpdatedClient = new BusinessClient
            {
                Id = userId,
                UserName = updateDto.UserName,
                Email = updateDto.Email,
                TaxID = updateDto.TaxId
            };

            _businessClientRepository.Setup(x => x.UpdateUserAsync(userId, It.IsAny<BusinessClient>()))
                                   .ReturnsAsync(expectedUpdatedClient);

            // Act
            var result = await _businessClientService.UpdateBusinessClientAsync(userId, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUpdatedClient.Id, result.Id);
            Assert.Equal(expectedUpdatedClient.UserName, result.UserName);
            Assert.Equal(expectedUpdatedClient.Email, result.Email);
            Assert.Equal(expectedUpdatedClient.TaxID, result.TaxID);
            _businessClientRepository.Verify(x => x.UpdateUserAsync(userId, It.Is<BusinessClient>(bc =>
                bc.UserName == updateDto.UserName &&
                bc.Email == updateDto.Email &&
                bc.TaxID == updateDto.TaxId)), Times.Once);
        }

        [Fact]
        public async Task UpdateBusinessClientAsync_CallsRepositoryWithCorrectParameters()
        {
            // Arrange
            var userId = 1;
            var updateDto = new UpdateBusinessClientDto("TestClient", "test@example.com", "123456789");

            var expectedUpdatedClient = new BusinessClient
            {
                Id = userId,
                UserName = updateDto.UserName,
                Email = updateDto.Email,
                TaxID = updateDto.TaxId
            };

            _businessClientRepository.Setup(x => x.UpdateUserAsync(It.IsAny<int>(), It.IsAny<BusinessClient>()))
                                   .ReturnsAsync(expectedUpdatedClient);

            // Act
            await _businessClientService.UpdateBusinessClientAsync(userId, updateDto);

            // Assert
            _businessClientRepository.Verify(x => x.UpdateUserAsync(
                userId,
                It.Is<BusinessClient>(bc =>
                    bc.UserName == updateDto.UserName &&
                    bc.Email == updateDto.Email &&
                    bc.TaxID == updateDto.TaxId)), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task UpdateBusinessClientAsync_HandlesInvalidUserId(int invalidUserId)
        {
            // Arrange
            var updateDto = new UpdateBusinessClientDto("TestClient", "test@example.com", "123456789");

            var expectedResult = new BusinessClient
            {
                UserName = updateDto.UserName,
                Email = updateDto.Email,
                TaxID = updateDto.TaxId
            };

            _businessClientRepository.Setup(x => x.UpdateUserAsync(invalidUserId, It.IsAny<BusinessClient>()))
                                   .ReturnsAsync(expectedResult);

            // Act
            var result = await _businessClientService.UpdateBusinessClientAsync(invalidUserId, updateDto);

            // Assert
            Assert.NotNull(result);
            _businessClientRepository.Verify(x => x.UpdateUserAsync(invalidUserId, It.IsAny<BusinessClient>()), Times.Once);
        }

        #endregion
    }
}