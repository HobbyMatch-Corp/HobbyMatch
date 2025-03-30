namespace HobbyMatch.Database.Repositories.BusinessClient
{
    public interface IBusinessClientRepository
    {
        public Task<List<Domain.Entities.BusinessClient>> GetBusinessClientsAsync();

        public Task<Domain.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task UpdateUserAsync(int businessClientId, Domain.Entities.BusinessClient businessClient);
    }
}
