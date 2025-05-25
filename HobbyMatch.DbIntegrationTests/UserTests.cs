using HobbyMatch.BL.DTOs.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Database.Repositories.Users;
using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.DbIntegrationTests
{
    public class UserTests : BaseIntegrationTest
    {

        public UserTests(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        [Fact]
        public async Task RegisterUser_VaildData_ShouldSucceed()
        {
            // Arrange
            var email = "integrationtest1@test.com";
            var pass = "IntegTest1!";
            var username = "IntegTestUser";
            var userRegisterRequest = new UserRegisterDto(email, pass, username);

            var userRepo = new UserRepository(DbContext);
            var accountService = new AccountService(null, UserManager, userRepo);

            // Act
            await accountService.RegisterUserAsync(userRegisterRequest);

            // Assert
            var user = await DbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.NotEqual(pass, user.PasswordHash);
            Assert.Equal(username, user.UserName);
        }
    }
}
