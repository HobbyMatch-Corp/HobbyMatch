using HobbyMatch.BL.Services.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Database.Repositories.User;
using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

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
            var pass = "IntegTest1";
            var username = "username";
            var userRegisterRequest = new UserRegisterRequest(email, pass, username);

            // Arrange.Mock
            var userManager = new Mock<UserManager<Organizer>>();
            var userRepo = new UserRepository(DbContext);
            var tokenGen = new Mock<JwtTokenGenerator>();
            var accountService = new AccountService(tokenGen.Object, userManager.Object, userRepo);

            // Act
            await accountService.RegisterUserAsync(userRegisterRequest);

            // Assert
            var user = await DbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            Assert.NotNull(user);
            Assert.Equal(user.Email, email);
            Assert.NotEqual(user.PasswordHash, pass);
            Assert.Equal(user.UserName, username);
        }
    }
}
