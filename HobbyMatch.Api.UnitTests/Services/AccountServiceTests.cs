using HobbyMatch.BL.Services.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Database.Repositories.Users;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Exceptions.AuthExceptions;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Identity;
using Moq;
using MockQueryable;

namespace UnitTests
{
	public class AccountServiceTests
	{
		private IAccountService _accountService;
		private Mock<ITokenGenerator> _tokenGenerator;
		private Mock<UserManager<HobbyMatch.Domain.Entities.Organizer>> _userManager;
		private Mock<IUserRepository> _userRepository;
		public AccountServiceTests()
		{
			_tokenGenerator = new Mock<ITokenGenerator>();

			var store = new Mock<IUserStore<Organizer>>();
			_userManager = new Mock<UserManager<Organizer>>(
				store.Object,
				null, // IOptions<IdentityOptions>
				null, // IPasswordHasher<TUser>
				new IUserValidator<Organizer>[0],
				new IPasswordValidator<Organizer>[0],
				null, // ILookupNormalizer
				null, // IdentityErrorDescriber
				null, // IServiceProvider
				null  // ILogger<UserManager<T>>
			);
			_userRepository = new Mock<IUserRepository>();

			_accountService = new AccountService(_tokenGenerator.Object, _userManager.Object, _userRepository.Object);
		}
		[Fact]
		public async Task RegisterBusinessClientAsync_ThrowsUserAlreadyExists_WhenUserWithThisMailAlreadyExists()
		{
			// Arrange
			BusinessRegisterRequest request = new BusinessRegisterRequest("test@example.com", "Passwort", "TaxId", "TestUserName");
			var existingUsers = new List<BusinessClient> { new BusinessClient { Email = "test@example.com" } }.AsQueryable().BuildMock();
			_userManager.Setup(x => x.Users).Returns(existingUsers);

			// Act & Assert
			await Assert.ThrowsAsync<UserAlreadyExistsException>(() => _accountService.RegisterBusinessClientAsync(request));
		}

		[Fact]
		public async Task RegisterBusinessClientAsync_CreatesUser_WhenCorrectDataIsPassed()
		{
			// Arrange
			var request = new BusinessRegisterRequest("test@example.com", "SecurePassword123", "1234567890", "TestUser");

			var emptyUsers = new List<BusinessClient>().AsQueryable().BuildMock();
			_userManager.Setup(x => x.Users).Returns(emptyUsers);

			_userManager.Setup(x => x.CreateAsync(It.IsAny<BusinessClient>(), request.Password))
				.ReturnsAsync(IdentityResult.Success);

			// Act
			await _accountService.RegisterBusinessClientAsync(request);

			// Assert
			_userManager.Verify(x => x.CreateAsync(
				It.Is<BusinessClient>(u =>
					u.Email == request.Email &&
					u.TaxID == request.TaxId &&
					u.UserName == request.UserName),
				request.Password), Times.Once);

		}
		[Fact]
		public async Task LoginUserAsync_ReturnsAuthResult_WhenCredentialsAreValid()
		{
			// Arrange
			var request = new LoginRequest("valid@example.com", "ValidPassword");
			var user = new User { Email = request.Email };

			_userManager.Setup(x => x.FindByEmailAsync(request.Email))
				.ReturnsAsync(user);
			_userManager.Setup(x => x.CheckPasswordAsync(user, request.Password))
				.ReturnsAsync(true);

			_tokenGenerator.Setup(x => x.GenerateToken(user))
				.Returns(("jwt_token_here", DateTime.UtcNow.AddMinutes(15)));

			_tokenGenerator.Setup(x => x.GenerateRefreshToken())
				.Returns("refresh_token_here");

			_userManager.Setup(x => x.UpdateAsync(user))
				.ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _accountService.LoginUserAsync(request);

			// Assert
			Assert.Equal("jwt_token_here", result.JwtToken);
			Assert.Equal("refresh_token_here", result.RefreshToken);
			Assert.True(result.JwtTokenExpirationDate > DateTime.UtcNow);
			Assert.True(result.RefreshTokenExpirationDate > DateTime.UtcNow);
			_userManager.Verify(x => x.UpdateAsync(user), Times.Once);
		}
		[Fact]
		public async Task LoginUserAsync_ThrowsLoginFailedException_WhenPasswordNotValid()
		{
			// Arrange
			var request = new LoginRequest("valid@example.com", "InvalidPassword");
			var user = new User { Email = request.Email };

			_userManager.Setup(x => x.FindByEmailAsync(request.Email))
				.ReturnsAsync(user);
			_userManager.Setup(x => x.CheckPasswordAsync(user, request.Password))
				.ReturnsAsync(false);

			// Act & Assert
			await Assert.ThrowsAsync<LoginFailedException>(() => _accountService.LoginUserAsync(request));
		}
		[Fact]
		public async Task LoginUserAsync_ThrowsLoginFailedException_WhenUserNotFoundNotValid()
		{
			// Arrange
			var request = new LoginRequest("Invalid@example.com", "InvalidPassword");

			_userManager.Setup(x => x.FindByEmailAsync(request.Email))
				.ReturnsAsync((Organizer?)null);

			// Act & Assert
			await Assert.ThrowsAsync<LoginFailedException>(() => _accountService.LoginUserAsync(request));
		}

	}
}
