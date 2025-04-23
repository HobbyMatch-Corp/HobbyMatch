using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.AppUsers
{
    public class MockAppUserService: IAppUserService
    {
        private User _user { get; set; }

        public MockAppUserService()
        {
            _user = new User();
            _user.UserName = "Jan Kowalski";
            _user.Id = 123;
            _user.Email = "jkowalski@test.com";
            _user.PasswordHash = "123!Abcd";
        }
         
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return _user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return _user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return new List<User> { _user };
        }

        public async Task UpdateUserAsync(int userId, User user)
        {
            _user = user;
        }
    }
}
