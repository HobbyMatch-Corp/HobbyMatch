using HobbyMatch.Domain.Entities;
using Moq;
using HobbyMatch.BL.Services.AppUsers;
using HobbyMatch.Database.Repositories.AppUsers;

namespace UnitTests
{
	public class AppUserServiceTests
	{
		private IAppUserService _appUserService;
		private Mock<IAppUserRepository> _appUserRepository;

		public AppUserServiceTests()
		{
			_appUserRepository = new Mock<IAppUserRepository>();
			_appUserService = new AppUserService(_appUserRepository.Object, null);
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
	}
}
