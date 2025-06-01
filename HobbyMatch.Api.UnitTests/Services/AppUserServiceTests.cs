using HobbyMatch.Domain.Entities;
using Moq;
using HobbyMatch.BL.Services.AppUsers;
using HobbyMatch.Database.Repositories.AppUsers;
using HobbyMatch.BL.DTOs.Organizers;
using Microsoft.EntityFrameworkCore;
using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.BL.Services.Hobbies;

namespace UnitTests
{
	public class AppUserServiceTests
	{
		private IAppUserService _appUserService;
		private Mock<IAppUserRepository> _appUserRepository;
		private Mock<IHobbyService> _mockHobbyService;

		public AppUserServiceTests()
		{
			_appUserRepository = new Mock<IAppUserRepository>();
			_mockHobbyService = new Mock<IHobbyService>();
			_appUserService = new AppUserService(_appUserRepository.Object, _mockHobbyService.Object);
		}

		#region AddFriendsAsync Tests

		[Fact]
		public async Task AddFriendsAsync_ReturnsTrue_WhenFriendAddedSuccessfully()
		{
			// Arrange
			var user = new User { Id = 10 };
			var friend = new User { Id = 20 };

			_appUserRepository.Setup(x => x.GetUserByIdAsync(friend.Id))
							.ReturnsAsync(friend);
			_appUserRepository.Setup(x => x.AddFriendToUserAsync(user, friend))
							.ReturnsAsync(true);
			_appUserRepository.Setup(x => x.AddFriendToUserAsync(friend, user))
							.ReturnsAsync(true);

			// Act
			var result = await _appUserService.AddFriendsAsync(friend.Id, user);

			// Assert
			Assert.True(result);
			_appUserRepository.Verify(x => x.GetUserByIdAsync(friend.Id), Times.Once);
		}

		[Fact]
		public async Task AddFriendsAsync_ReturnsFalse_WhenFriendDoesNotExist()
		{
			// Arrange
			var user = new User { Id = 10 };
			var friend = new User { Id = 20 };

			_appUserRepository.Setup(x => x.GetUserByIdAsync(friend.Id))
							.ReturnsAsync((User)null);
			_appUserRepository.Setup(x => x.AddFriendToUserAsync(user, friend))
							.ReturnsAsync(true);
			_appUserRepository.Setup(x => x.AddFriendToUserAsync(friend, user))
							.ReturnsAsync(true);

			// Act
			var result = await _appUserService.AddFriendsAsync(friend.Id, user);

			// Assert
			Assert.False(result);
			_appUserRepository.Verify(x => x.GetUserByIdAsync(friend.Id), Times.Once);
		}

		[Fact]
		public async Task AddFriendsAsync_ReturnsFalse_WhenAlreadyFriends()
		{
			// Arrange
			var user = new User { Id = 10 };
			var friend = new User { Id = 20 };
			user.Friends.Add(friend);
			friend.Friends.Add(user);

			_appUserRepository.Setup(x => x.GetUserByIdAsync(friend.Id))
							.ReturnsAsync(friend);
			_appUserRepository.Setup(x => x.AddFriendToUserAsync(user, friend))
							.ReturnsAsync(true);
			_appUserRepository.Setup(x => x.AddFriendToUserAsync(friend, user))
							.ReturnsAsync(true);

			// Act
			var result = await _appUserService.AddFriendsAsync(friend.Id, user);

			// Assert
			Assert.False(result);
			_appUserRepository.Verify(x => x.GetUserByIdAsync(friend.Id), Times.Once);
		}

		#endregion

		#region RemoveUserFromEventAsync Tests

		[Fact]
		public async Task RemoveFriendsAsync_ReturnsTrue_WhenRemovedFriendsSuccessfully()
		{
			// Arrange
			var user = new User { Id = 10 };
			var friend = new User { Id = 20 };
			user.Friends.Add(friend);
			friend.Friends.Add(user);

			_appUserRepository.Setup(x => x.RemoveFriendFromUserAsync(user, friend))
							.ReturnsAsync(true);
			_appUserRepository.Setup(x => x.RemoveFriendFromUserAsync(friend, user))
							.ReturnsAsync(true);

			// Act
			var result = await _appUserService.RemoveFriendsAsync(friend.Id, user);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async Task RemoveFriendsAsync_ReturnsFalse_WhenFriendDoesNotExist()
		{
			// Arrange
			var user = new User { Id = 10 };
			var friend = new User { Id = 20 };

			_appUserRepository.Setup(x => x.RemoveFriendFromUserAsync(user, friend))
							.ReturnsAsync(true);
			_appUserRepository.Setup(x => x.RemoveFriendFromUserAsync(friend, user))
							.ReturnsAsync(true);

			// Act
			var result = await _appUserService.RemoveFriendsAsync(friend.Id, user);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async Task RemoveUserFriendsAsync_ReturnsFalse_WhenNotFriends()
		{
			// Arrange
			var user = new User { Id = 10 };
			var friend = new User { Id = 20 };

			_appUserRepository.Setup(x => x.RemoveFriendFromUserAsync(user, friend))
							.ReturnsAsync(true);
			_appUserRepository.Setup(x => x.RemoveFriendFromUserAsync(friend, user))
							.ReturnsAsync(true);

			// Act
			var result = await _appUserService.RemoveFriendsAsync(friend.Id, user);

			// Assert
			Assert.False(result);
		}

		#endregion

		#region UpdateUser Tests
		[Fact]
		public async Task UpdateUserAsync_UserExists_UpdatesAndReturnsUser()
		{
			// Arrange
			int userId = 1;
			var userDto = new UpdateUserDto("newUser", "new@example.com", new[] { new HobbyDto("Reading"), new HobbyDto("Gaming") });

			var user = new User { Id = userId, Email = "old@example.com", UserName = "oldUser", Hobbies = new List<Hobby>() };
			var hobbies = new List<Hobby> { new Hobby { Name = "Reading" }, new Hobby { Name = "Gaming" } };

			_appUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
			_mockHobbyService.Setup(h => h.GetHobbiesAsync(It.IsAny<List<HobbyDto>>())).ReturnsAsync(hobbies);
			_appUserRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

			// Act
			var updatedUser = await _appUserService.UpdateUserAsync(userId, userDto);

			// Assert
			Assert.Equal(userDto.email, updatedUser.Email);
			Assert.Equal(userDto.userName, updatedUser.UserName);
			Assert.Equal(2, updatedUser.Hobbies.Count);
		}

		[Fact]
		public async Task UpdateUserAsync_UserNotFound_ThrowsDbUpdateException()
		{
			// Arrange
			int userId = 1;
			var userDto = new UpdateUserDto("testUser", "test@example.com", new[] { new HobbyDto("Reading") });

			_appUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null!);

			// Act & Assert
			await Assert.ThrowsAsync<DbUpdateException>(() => _appUserService.UpdateUserAsync(userId, userDto));
		}
		#endregion

		#region GetUser Tests
		[Fact]
		public async Task GetUserByEmailAsync_ReturnsUser()
		{
			// Arrange
			var email = "test@example.com";
			var expectedUser = new User { Email = email };
			_appUserRepository.Setup(repo => repo.GetUserByEmailAsync(email))
					 .ReturnsAsync(expectedUser);

			// Act
			var result = await _appUserService.GetUserByEmailAsync(email);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(email, result!.Email);
		}

		[Fact]
		public async Task GetUserByIdAsync_ReturnsUser()
		{
			// Arrange
			var id = 1;
			var expectedUser = new User { Id = id };
			_appUserRepository.Setup(repo => repo.GetUserByIdAsync(id))
					 .ReturnsAsync(expectedUser);

			// Act
			var result = await _appUserService.GetUserByIdAsync(id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(id, result!.Id);
		}

		[Fact]
		public async Task GetUsersAsync_ReturnsUserList()
		{
			// Arrange
			var expectedUsers = new List<User> { new User { Id = 1 }, new User { Id = 2 } };
			_appUserRepository.Setup(repo => repo.GetUsersAsync())
					 .ReturnsAsync(expectedUsers);

			// Act
			var result = await _appUserService.GetUsersAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count);
		}
		#endregion
	}
}
