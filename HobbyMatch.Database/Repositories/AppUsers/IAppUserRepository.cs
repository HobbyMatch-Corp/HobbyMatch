namespace HobbyMatch.Database.Repositories.AppUsers
{
    public interface IAppUserRepository
    {
        public Task<List<Domain.Entities.User>> GetUsersAsync();

        public Task<Domain.Entities.User?> GetUserByEmailAsync(string email);

        public Task<Domain.Entities.User?> GetUserByIdAsync(int id);

        public Task UpdateUserAsync(int id, Domain.Entities.User user);
    }
}
